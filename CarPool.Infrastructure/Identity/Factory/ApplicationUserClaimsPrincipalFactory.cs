using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarPool.Infrastructure.Identity;

internal class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim(ApplicationUser.FullNameClaimType, user.Name));

        var roles = await UserManager.GetRolesAsync(user);
        foreach (var role in roles)
            identity.AddClaim(new Claim(ClaimTypes.Role, role));

        return identity;
    }
}
