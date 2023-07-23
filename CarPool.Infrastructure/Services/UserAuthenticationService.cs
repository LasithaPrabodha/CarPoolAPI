using CarPool.Application;
using CarPool.Application.DTOs;
using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarPool.Application.Models;
using Microsoft.Extensions.Options;
using CarPool.Application.Common.Constants;

namespace CarPool.Infrastructure.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserAuthenticationService> _logger;
    private readonly JwtSettings _jwtSettings;

    public UserAuthenticationService(ILogger<UserAuthenticationService> logger, SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings) {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthResponseDTO>> Login(LoginRequestDTO loginRequest) {
        var serviceResult = new Result<AuthResponseDTO>();
        try {
            var applicationUser = await _userManager.FindByNameAsync(loginRequest?.Username);
            var validationResult = ValidateUserForLogin(applicationUser);
            if (validationResult.Failed)
                return validationResult;

            return await SignInUser(applicationUser, loginRequest);
        }
        catch (Exception ex) {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to sign in user.");
        }
        return serviceResult;
    }

    public async Task<Result> Logout() {
        var serviceResult = new Result<string>();
        try {
            await _signInManager.SignOutAsync();
            serviceResult.Successful().WithMessage("Successfully logged out.");
        }
        catch (Exception ex) {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to sign out user.");
        }
        return serviceResult;
    }

    private Result<AuthResponseDTO> ValidateUserForLogin(ApplicationUser applicationUser) {
        var serviceResult = new Result<AuthResponseDTO>();
        if (applicationUser == null)
            return serviceResult.Failed().WithMessage("User not found.");

        if (applicationUser.IsActive == false)
            return serviceResult.Failed().WithMessage("User is not active.");
        return serviceResult;
    }

    private async Task<Result<AuthResponseDTO>> SignInUser(ApplicationUser applicationUser, LoginRequestDTO loginRequest) {
        var serviceResult = new Result<AuthResponseDTO>();
        var loginResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequest.Password, false);
        if (loginResult.Succeeded)
        {
            JwtSecurityToken jwtSecurityToken = await GenerateToken(applicationUser);

            var response = new AuthResponseDTO
            {
                Id = applicationUser.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = applicationUser.Email,
                UserName = applicationUser.UserName
            };

            serviceResult.Successful().WithData(response);
        }
        else
            serviceResult.Failed().WithMessage($"Unable to login.");
        return serviceResult;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
