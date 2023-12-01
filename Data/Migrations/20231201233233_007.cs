using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinemanage.Data.Migrations
{
    /// <inheritdoc />
    public partial class _007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "MovieCollection");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MovieCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
