using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Persistence");

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "Persistence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Origin_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    PricePerSeat = table.Column<double>(type: "float", nullable: false),
                    ViewedByCount = table.Column<int>(type: "int", nullable: true),
                    Vehicle_Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Year = table.Column<int>(type: "int", nullable: true),
                    Vehicle_Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Persistence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: true),
                    ChatPreference = table.Column<int>(type: "int", nullable: true),
                    ScentsPreference = table.Column<int>(type: "int", nullable: true),
                    IsDriver = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "Persistence",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredSeats = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => new { x.BookingId, x.TripId });
                    table.ForeignKey(
                        name: "FK_Bookings_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Persistence",
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                schema: "Persistence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => new { x.Id, x.TripId });
                    table.ForeignKey(
                        name: "FK_Stops_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Persistence",
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverProfiles",
                schema: "Persistence",
                columns: table => new
                {
                    DriverProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vehicle_Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Year = table.Column<int>(type: "int", nullable: true),
                    Vehicle_Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverLicense_DriverLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLicense_DriverLicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPassengersDriven = table.Column<int>(type: "int", nullable: false),
                    TotalKmsShared = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverProfiles", x => new { x.DriverProfileId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DriverProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Persistence",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "Persistence",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => new { x.MessageId, x.BookingId, x.TripId });
                    table.ForeignKey(
                        name: "FK_Messages_Bookings_BookingId_TripId",
                        columns: x => new { x.BookingId, x.TripId },
                        principalSchema: "Persistence",
                        principalTable: "Bookings",
                        principalColumns: new[] { "BookingId", "TripId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "Persistence",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => new { x.NotificationId, x.BookingId, x.TripId });
                    table.ForeignKey(
                        name: "FK_Notifications_Bookings_BookingId_TripId",
                        columns: x => new { x.BookingId, x.TripId },
                        principalSchema: "Persistence",
                        principalTable: "Bookings",
                        principalColumns: new[] { "BookingId", "TripId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "Persistence",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.ReviewId, x.BookingId, x.TripId });
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId_TripId",
                        columns: x => new { x.BookingId, x.TripId },
                        principalSchema: "Persistence",
                        principalTable: "Bookings",
                        principalColumns: new[] { "BookingId", "TripId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripId",
                schema: "Persistence",
                table: "Bookings",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverProfiles_UserId",
                schema: "Persistence",
                table: "DriverProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BookingId_TripId",
                schema: "Persistence",
                table: "Messages",
                columns: new[] { "BookingId", "TripId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BookingId_TripId",
                schema: "Persistence",
                table: "Notifications",
                columns: new[] { "BookingId", "TripId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId_TripId",
                schema: "Persistence",
                table: "Reviews",
                columns: new[] { "BookingId", "TripId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TripId",
                schema: "Persistence",
                table: "Stops",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverProfiles",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Stops",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "Persistence");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "Persistence");
        }
    }
}
