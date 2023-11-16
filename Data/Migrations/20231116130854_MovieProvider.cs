using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinemanage.Data.Migrations
{
    /// <inheritdoc />
    public partial class MovieProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProviderLogo",
                table: "Movie",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderLogoType",
                table: "Movie",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieProvider_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieProvider_MovieId",
                table: "MovieProvider",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieProvider");

            migrationBuilder.DropColumn(
                name: "ProviderLogo",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ProviderLogoType",
                table: "Movie");
        }
    }
}
