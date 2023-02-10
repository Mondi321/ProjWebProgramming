using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjWebProgramming.Migrations
{
    public partial class photoUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Movies");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageCarousel",
                table: "TvShows",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageCarousel",
                table: "Movies",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCarousel",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "ImageCarousel",
                table: "Movies");

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
    }
}
