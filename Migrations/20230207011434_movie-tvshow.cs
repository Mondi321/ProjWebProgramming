using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjWebProgramming.Migrations
{
    public partial class movietvshow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TvShows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Movies");
        }
    }
}
