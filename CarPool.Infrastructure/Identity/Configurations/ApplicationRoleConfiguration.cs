using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Infrastructure.Identity;

internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder) {
        builder.ToTable("Roles");

        builder.HasData(
            new ApplicationRole() { Id = "e5adff57-b654-4f30-b6a7-c818e86cda8e", ConcurrencyStamp = "6a1bfaad-4414-4593-895c-a100aedd1741", Name = "User", NormalizedName = "USER" },
            new ApplicationRole() { Id = "40e668a2-8a53-4907-817c-e4f8c8f72fb4", ConcurrencyStamp = "b47ee50f-0b94-42bd-858c-2f4bacd4bb50", Name = "Driver", NormalizedName = "DRIVER" },
            new ApplicationRole() { Id = "8fa3842a-98a4-475b-8926-fce6efdc3e6f", ConcurrencyStamp = "b3a92cb9-8d66-47d4-9670-4e110447b887", Name = "Admin", NormalizedName = "ADMIN" }
            );
    }
}
