using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdentityIdRemovedFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "Persistence",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                schema: "Persistence",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
