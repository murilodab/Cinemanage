using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinemanage.Data.Migrations
{
    /// <inheritdoc />
    public partial class _005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieCollectionId",
                table: "MovieCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_MovieCollectionId",
                table: "MovieCollection",
                column: "MovieCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_MovieCollection_MovieCollectionId",
                table: "MovieCollection",
                column: "MovieCollectionId",
                principalTable: "MovieCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_MovieCollection_MovieCollectionId",
                table: "MovieCollection");

            migrationBuilder.DropIndex(
                name: "IX_MovieCollection_MovieCollectionId",
                table: "MovieCollection");

            migrationBuilder.DropColumn(
                name: "MovieCollectionId",
                table: "MovieCollection");
        }
    }
}
