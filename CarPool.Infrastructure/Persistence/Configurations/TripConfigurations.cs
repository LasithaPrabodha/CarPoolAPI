using System;
using CarPool.Domain.Trips;
using CarPool.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Infrastructure.Persistence;

public class TripConfigurations : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        ConfigureTripsTable(builder);
        ConfigureBookingsTable(builder);
        ConfigureStopsTable(builder);
    }

    private static void ConfigureTripsTable(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(u => u.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TripId.Create(value));
        builder
           .Property(b => b.DriverId)
           .HasConversion(
               id => id.Value,
               value => UserId.Create(value));

        builder.Property(t => t.DriverId).IsRequired();
        builder.Property(t => t.DepartTime).IsRequired();
        builder.Property(t => t.AvailableSeats).IsRequired();
        builder.Property(t => t.PricePerSeat).IsRequired();

        builder.OwnsOne(t => t.Origin);
        builder.OwnsOne(t => t.Destination);

        builder.OwnsOne(t => t.Vehicle);

    }

    private static void ConfigureBookingsTable(EntityTypeBuilder<Trip> builder)
    {
        builder.OwnsMany(t => t.Bookings, bBuilder =>
        {
            bBuilder.ToTable("Bookings");

            bBuilder
                .WithOwner()
                .HasForeignKey("TripId");

            bBuilder.HasKey("Id", "TripId");

            bBuilder
                .Property(bBuilder => bBuilder.Id)
                .HasColumnName("BookingId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => BookingId.Create(value));

            bBuilder
                .Property(m => m.UserId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            bBuilder.Property(b => b.RequiredSeats).IsRequired();
            bBuilder.Property(b => b.UserId).IsRequired();

            bBuilder.OwnsOne(
                b => b.Review,
                rBuilder => ConfigureReviewTable(rBuilder));

            bBuilder.Navigation(b => b.Review).IsRequired(false);

            bBuilder.OwnsMany(
                s => s.Messages,
                msgBuilder => ConfigureMessagesTable(msgBuilder));


            bBuilder.OwnsMany(
                s => s.Notifications,
                nBuilder => ConfigureNotificationTable(nBuilder));


            bBuilder
                .Navigation(s => s.Messages).Metadata
                .SetField("_messages");

            bBuilder
                .Navigation(s => s.Messages)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        });

        builder.Metadata
           .FindNavigation(nameof(Trip.Bookings))!
           .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureStopsTable(EntityTypeBuilder<Trip> builder)
    {
        builder.OwnsMany(t => t.Stops, sBuilder =>
        {

            sBuilder.ToTable("Stops");

            sBuilder
                .WithOwner()
                .HasForeignKey("TripId");

            sBuilder.HasKey("Id", "TripId");

            sBuilder
                .Property(bBuilder => bBuilder.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => StopId.Create(value));

            sBuilder.OwnsOne(u => u.Address);
        });
    }
    private static void ConfigureMessagesTable(OwnedNavigationBuilder<Booking, Message> msgBuilder)
    {
        msgBuilder.ToTable("Messages");

        msgBuilder
            .WithOwner()
            .HasForeignKey("BookingId", "TripId");

        msgBuilder.HasKey(nameof(Message.Id), "BookingId", "TripId");

        msgBuilder
            .Property(msg => msg.Id)
            .HasColumnName("MessageId")
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => MessageId.Create(value)
            );

        msgBuilder
            .Property(m => m.SenderId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        msgBuilder
            .Property(m => m.ReceiverId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        msgBuilder.Property(m => m.MessageText);
    }

    private static void ConfigureNotificationTable(OwnedNavigationBuilder<Booking, Notification> nBuilder)
    {
        nBuilder.ToTable("Notifications");

        nBuilder
            .WithOwner()
            .HasForeignKey("BookingId", "TripId");

        nBuilder.HasKey(nameof(Notification.Id), "BookingId", "TripId");

        nBuilder
            .Property(n => n.Id)
            .HasColumnName("NotificationId")
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => NotificationId.Create(value)
            );

        nBuilder
           .Property(m => m.ToUserId)
           .HasConversion(
               id => id.Value,
               value => UserId.Create(value));

        nBuilder.Property(n => n.NotificationText).IsRequired(true);

        nBuilder.Property(n => n.IsRead).IsRequired(false);


    }
    private static void ConfigureReviewTable(OwnedNavigationBuilder<Booking, Review> rBuilder)
    {
        rBuilder.ToTable("Reviews");

        rBuilder
            .WithOwner()
            .HasForeignKey("BookingId", "TripId");

        rBuilder.HasKey(nameof(Review.Id), "BookingId", "TripId");

        rBuilder
            .Property(n => n.Id)
            .HasColumnName("ReviewId")
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => ReviewId.Create(value)
            );

        rBuilder
            .Property(m => m.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        rBuilder.Property(r => r.Rating).IsRequired(true);
        rBuilder.Property(r => r.ReviewText).IsRequired(true);
    }
}

/**
 * , navigationBuilder =>
            {
                navigationBuilder
                    .ToTable("StopAddresses");

                navigationBuilder
                    .WithOwner()
                    .HasForeignKey("UserId");
                    
                navigationBuilder
                    .Property<Guid>("Id");

                navigationBuilder
                    .HasKey("Id"); 

                navigationBuilder
                    .Property(address => address.PostalCode)
                    .HasMaxLength(7)
                    .IsRequired()
                    .IsUnicode(false);

                navigationBuilder
                    .Property(address => address.Street)
                    .HasMaxLength(128)
                    .IsRequired()
                    .IsUnicode(false);

                navigationBuilder
                    .Property(address => address.Province)
                    .HasMaxLength(10)
                    .IsRequired()
                    .IsUnicode(false);

                navigationBuilder
                    .Property(address => address.City)
                    .HasMaxLength(20)
                    .IsRequired()
                    .IsUnicode(false);
            }
 */