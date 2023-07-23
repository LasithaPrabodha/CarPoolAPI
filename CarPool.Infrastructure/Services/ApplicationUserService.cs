using System.Linq.Dynamic.Core;
using System.Transactions;
using CarPool.Application;
using CarPool.Application.DTOs;
using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Users;
using CarPool.Domain.Users.Enums;
using CarPool.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MapsterMapper;
using Mapster;
using CarPool.Domain.Common;

namespace CarPool.Infrastructure.Services;

internal class ApplicationUserService : IApplicationUserService
{
    private readonly ILogger<ApplicationUserService> _logger;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public ApplicationUserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<ApplicationUserService> logger, IMapper mapper, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, IAuthorizationService authorizationService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<Result> ActivateUser(string username)
    {
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            var validationResult = ValidateUserForActivation(applicationUser);
            if (validationResult.Failed)
                return validationResult;

            applicationUser.IsActive = true;
            var result = await _userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to activate user.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to activate user.");
        }
        return serviceResult;
    }

    public async Task<Result> DeactivateUser(string username)
    {
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
                return serviceResult.Failed().WithMessage("User not found.");

            if (!applicationUser.IsActive)
                return serviceResult.Failed().WithMessage("User is not active.");

            applicationUser.IsActive = false;
            var result = await _userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to deactivate user.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to deactivate user.");
        }

        return serviceResult;
    }

    public async Task<Result> AddRoles(RoleAssignmentRequestDTO request)
    {
        var serviceResult = new Result().Successful();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.Username);

            if (applicationUser == null)
                return serviceResult.Failed().WithMessage("User not found.");

            var validationResult = await ValidateRolesForAdditionAsync(request.Roles, applicationUser);
            if (validationResult.Failed)
                return validationResult;

            var result = await _userManager.AddToRolesAsync(applicationUser, request.Roles.Select(nr => nr.ToUpper()));

            if (result.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to add role user.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to add role to user.");
        }

        return serviceResult;
    }

    public async Task<Result> RemoveRoles(RoleAssignmentRequestDTO request)
    {
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.Username);
            if (applicationUser == null)
                return serviceResult.Failed().WithMessage("User not found.");

            var validationResult = await ValidateRolesForRemovalAsync(request.Roles, applicationUser);
            if (validationResult.Failed)
                return validationResult;

            var result = await _userManager.RemoveFromRolesAsync(applicationUser, request.Roles.Select(r => r.ToUpper()));

            if (result.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to remove role from user.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to remove role from user.");
        }

        return serviceResult;
    }

    public async Task<Result> CreateUser(UserDTO user, string password, List<string> roles, bool isActive)
    {
        var serviceResult = new Result().Successful();
        try
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            applicationUser.IsActive = isActive;

            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using (transaction)
            {
                var identityResult = await _userManager.CreateAsync(applicationUser, password);

                if (!identityResult.Succeeded)
                {
                    serviceResult.Failed().WithErrors(_mapper.Map<List<ResultError>>(identityResult.Errors));
                }
                else if (roles?.Any() ?? false)
                {
                    var rolesResult =
                        await _userManager.AddToRolesAsync(applicationUser, roles.Select(nr => nr.ToUpper()));
                    if (!rolesResult.Succeeded)
                        serviceResult.Failed().WithErrors(_mapper.Map<List<ResultError>>(identityResult.Errors));
                    else
                        serviceResult.Successful().AddMetadata("UserId", applicationUser.Id);
                }
                else
                    serviceResult.Successful().AddMetadata("UserId", applicationUser.Id);

                transaction.Complete();
            }
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create user.");
        }
        return serviceResult;
    }

    public async Task<Result> ChangePassword(string username, string password)
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(username);
            if (applicationUser == null)
            {
                return serviceResult.Failed().WithMessage("User not found.");
            }

            var hashResult = passwordHasher.VerifyHashedPassword(applicationUser, applicationUser.PasswordHash, password);
            if (hashResult == PasswordVerificationResult.Success)
            {
                return serviceResult.Failed().WithMessage("Please use a different password.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, password);

            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(applicationUser);

                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(applicationUser, password, false, false);

                serviceResult.Successful();
            }
            else
            {
                serviceResult.Failed().WithMessage($"Unable to change user password.");
            }
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to change user password.");
        }

        return serviceResult;
    }

    public async Task<Result<UserDTO?>> GetUser(string userId)
    {
        var serviceResult = new Result<UserDTO?>();
        try
        {
            var applicationUser = await GetUsersWithRoles().FirstOrDefaultAsync(u => u.Id == userId);

            if (applicationUser is null)
            {
                throw new UserNotFoundException(userId);
            }
            else
            {
                serviceResult.Successful();
                serviceResult.Data = _mapper.Map<UserDTO>(applicationUser);
            }
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get the user.");
        }

        return serviceResult;
    }

    public async Task<Result> UpdateRoles(string username, List<string> roles)
    {
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(username);
            if (applicationUser == null)
            {
                return serviceResult.Failed().WithMessage("User not found.");
            }

            var existingRoles = await _userManager.GetRolesAsync(applicationUser);
            var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, existingRoles);

            if (!identityResult.Succeeded) return serviceResult.Failed().WithMessage($"Unable to update user roles.");

            identityResult = await _userManager.AddToRolesAsync(applicationUser, roles.Select(r => r.ToUpper()));

            if (identityResult.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to update user roles.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to update roles of user.");
        }

        return serviceResult;
    }

    public async Task<Result> UpdateUserDetails(UpdateUserDetailsDTO request)
    {
        var serviceResult = new Result();
        try
        {
            var applicationUser = await _userManager.FindByIdAsync(request.Id);

            if (applicationUser == null)
                return serviceResult.Failed().WithMessage("User not found.");

            if (!string.IsNullOrWhiteSpace(request.Email) && applicationUser.Email != request.Email)
                applicationUser.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Name) && applicationUser.Name != request.Name)
                applicationUser.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && applicationUser.PhoneNumber != request.PhoneNumber)
                applicationUser.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"Unable to update user details.");
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to update user details.");
        }

        return serviceResult;
    }
    public async Task<Result> IsInRoleAsync(string userId, string role)
    {
        var serviceResult = new Result();
        try
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user != null && await _userManager.IsInRoleAsync(user, role))
                serviceResult.Successful();
            else
                serviceResult.Failed().WithMessage($"User not found or is not a {role}.");

        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to update user details.");
        }

        return serviceResult;
    }

    public async Task<Result> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new Result().Failed().WithMessage("User not found");
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded ? new Result().Successful() : new Result().Failed();
    }
    private IQueryable<ApplicationUser> GetUsersWithRoles()
    {
        return _userManager.Users.AsNoTracking().Include(u => u.Roles).ThenInclude(ur => ur.Role);
    }

    private static Result<string> ValidateUserForActivation(ApplicationUser applicationUser)
    {
        var serviceResult = new Result<string>();
        if (applicationUser == null)
            return serviceResult.Failed().WithMessage("User not found.");

        if (applicationUser.IsActive)
            return serviceResult.Failed().WithMessage("User is already active.");
        return serviceResult;
    }

    private async Task<Result> ValidateRolesForAdditionAsync(List<string> roles, ApplicationUser applicationUser)
    {
        var validationResult = new Result();
        var rolesAlreadyAssigned = await GetAlreadyAssignedRolesFromUserAsync(roles, applicationUser);
        var invalidRoles = GetInvalidRoles(roles);

        if (rolesAlreadyAssigned.Any())
            return validationResult.Failed().WithErrors(rolesAlreadyAssigned.Select(r => new ResultError($"Role {r} is already assigned.")).ToList());

        if (invalidRoles.Any())
            return validationResult.Failed().WithErrors(invalidRoles.Select(r => new ResultError($"Role {r} is invalid.")).ToList());

        return validationResult.Successful();
    }

    private async Task<Result> ValidateRolesForRemovalAsync(List<string> roles, ApplicationUser applicationUser)
    {
        var validationResult = new Result();
        var invalidRoles = GetInvalidRoles(roles);
        var rolesNotAssigned = await GetUnassignedRolesFromUserAsync(roles, applicationUser);

        if (rolesNotAssigned.Any())
            return validationResult.Failed().WithErrors(rolesNotAssigned.Select(r => new ResultError($"Role {r} is not assigned.")).ToList());

        if (invalidRoles.Any())
            return validationResult.Failed().WithErrors(invalidRoles.Select(r => new ResultError($"Role {r} is invalid.")).ToList());

        return validationResult.Successful();
    }

    private static List<string> GetInvalidRoles(List<string> rolesToCheck)
    {
        var invalidRoles = new List<string>();
        foreach (var roleToCheck in rolesToCheck)
        {
            var isRoleInvalid = !Enum.IsDefined(typeof(Roles), roleToCheck);
            if (isRoleInvalid)
                invalidRoles.Add(roleToCheck);
        }
        return invalidRoles;
    }

    private async Task<List<string>> GetUnassignedRolesFromUserAsync(List<string> rolesToCheck, ApplicationUser applicationUser)
    {
        var rolesNotAssigned = new List<string>();
        foreach (var roleToCheck in rolesToCheck)
        {
            bool isRoleAssigned = await _userManager.IsInRoleAsync(applicationUser, roleToCheck.ToUpper());
            if (isRoleAssigned == false)
                rolesNotAssigned.Add(roleToCheck);
        }
        return rolesNotAssigned;
    }

    private async Task<List<string>> GetAlreadyAssignedRolesFromUserAsync(List<string> rolesToCheck, ApplicationUser applicationUser)
    {
        var rolesAssigned = new List<string>();
        foreach (var roleToCheck in rolesToCheck)
        {
            if (await _userManager.IsInRoleAsync(applicationUser, roleToCheck.ToUpper()))
            {
                rolesAssigned.Add(roleToCheck);
            }
        }
        return rolesAssigned;
    }


}
