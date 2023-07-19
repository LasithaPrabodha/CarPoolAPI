using System;
using System.Reflection;
using CarPool.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Infrastructure.Persistence;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureDriverDetailsTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);


        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Username).IsRequired();
        builder.Property(u => u.Email).IsRequired();

        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .Property(m => m.Name)
            .HasMaxLength(100);


        builder.OwnsOne(u => u.Address);

    }

    private static void ConfigureDriverDetailsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.DriverProfile, dpBuilder =>
        {
            dpBuilder.ToTable("DriverProfiles");

            dpBuilder
                .WithOwner()
                .HasForeignKey("UserId");

            dpBuilder.HasKey("Id", "UserId");

            dpBuilder.Property(s => s.Id)
               .HasColumnName("DriverProfileId")
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => DriverProfileId.Create(value));

            dpBuilder.Property(dp => dp.TotalPassengersDriven).IsRequired();
            dpBuilder.Property(dp => dp.TotalPassengersDriven).IsRequired();

            dpBuilder.OwnsOne(dp => dp.Vehicle);
            dpBuilder.OwnsOne(dp => dp.DriverLicense);
        });

        builder.Navigation(u => u.DriverProfile).IsRequired(false);

        builder.Metadata
            .FindNavigation(nameof(User.DriverProfile))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}

