using System;
using System.Collections.Generic;
using CarPool.Common;
using Microsoft.AspNetCore.Identity;

namespace CarPool.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public const string FullNameClaimType = "user:fullname";

    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    public virtual ICollection<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();
}
public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}
public class ApplicationRole : IdentityRole
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
}