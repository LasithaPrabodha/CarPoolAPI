using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Infrastructure.Identity;

public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
        builder.ApplyConfiguration(new UserClaimsConfiguration());
        builder.ApplyConfiguration(new RoleClaimsConfiguration());
        builder.ApplyConfiguration(new UserLoginsConfiguration());
        builder.ApplyConfiguration(new UserTokensConfiguration());
    }
}

