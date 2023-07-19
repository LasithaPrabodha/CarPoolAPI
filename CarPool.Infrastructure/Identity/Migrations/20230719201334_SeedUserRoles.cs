using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPool.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40e668a2-8a53-4907-817c-e4f8c8f72fb4", "b47ee50f-0b94-42bd-858c-2f4bacd4bb50", "Driver", "DRIVER" },
                    { "8fa3842a-98a4-475b-8926-fce6efdc3e6f", "b3a92cb9-8d66-47d4-9670-4e110447b887", "Admin", "ADMIN" },
                    { "e5adff57-b654-4f30-b6a7-c818e86cda8e", "6a1bfaad-4414-4593-895c-a100aedd1741", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "40e668a2-8a53-4907-817c-e4f8c8f72fb4");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8fa3842a-98a4-475b-8926-fce6efdc3e6f");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e5adff57-b654-4f30-b6a7-c818e86cda8e");
        }
    }
}
