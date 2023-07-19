using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UsernameInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "Persistence",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                schema: "Persistence",
                table: "Users");
        }
    }
}
