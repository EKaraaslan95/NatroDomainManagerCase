using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatroDomainManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration110702 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Favorites",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Favorites");
        }
    }
}
