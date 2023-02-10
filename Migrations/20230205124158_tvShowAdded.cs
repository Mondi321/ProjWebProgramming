using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjWebProgramming.Migrations
{
    public partial class tvShowAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "TvShows",
                columns: table => new
                {
                    TvShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Seasons = table.Column<int>(type: "int", nullable: false),
                    Episodes = table.Column<int>(type: "int", nullable: false),
                    EpisodeLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShows", x => x.TvShowId);
                    table.ForeignKey(
                        name: "FK_TvShows_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "DirectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowActors",
                columns: table => new
                {
                    TvShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowActors", x => new { x.ActorId, x.TvShowId });
                    table.ForeignKey(
                        name: "FK_TvShowActors_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowActors_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowGenres",
                columns: table => new
                {
                    TvShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowGenres", x => new { x.TvShowId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_TvShowGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowGenres_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowWishlists",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TvShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowWishlists", x => new { x.TvShowId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TvShowWishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowWishlists_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvShowActors_TvShowId",
                table: "TvShowActors",
                column: "TvShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowGenres_GenreId",
                table: "TvShowGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_DirectorId",
                table: "TvShows",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowWishlists_UserId",
                table: "TvShowWishlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvShowActors");

            migrationBuilder.DropTable(
                name: "TvShowGenres");

            migrationBuilder.DropTable(
                name: "TvShowWishlists");

            migrationBuilder.DropTable(
                name: "TvShows");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
