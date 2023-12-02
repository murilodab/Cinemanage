using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinemanage.Data.Migrations
{
    /// <inheritdoc />
    public partial class _008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "MovieCollection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Collection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_AppUserId",
                table: "MovieCollection",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_AppUserId",
                table: "Collection",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_AspNetUsers_AppUserId",
                table: "Collection",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_AspNetUsers_AppUserId",
                table: "MovieCollection",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_AspNetUsers_AppUserId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_AspNetUsers_AppUserId",
                table: "MovieCollection");

            migrationBuilder.DropIndex(
                name: "IX_MovieCollection_AppUserId",
                table: "MovieCollection");

            migrationBuilder.DropIndex(
                name: "IX_Collection_AppUserId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MovieCollection");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
