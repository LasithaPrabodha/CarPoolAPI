
using System.Security.Claims;
using CarPool.Application.Contracts;
using CarPool.Domain.Users.Enums;
using CarPool.Common;
using CarPool.Application.Common.Constants;

namespace CarPool.WebAPI.Services;
 
 public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Uid)?.Value ?? null;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;

            var roles = httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == ClaimTypes.Role);
            if (roles != null)
                Roles = roles.Select(r => r.Value.ToEnum<Roles>());
        }

        public string UserId { get; }

        public string Username { get; }
        public IEnumerable<Roles> Roles { get; }
    }