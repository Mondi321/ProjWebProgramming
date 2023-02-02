using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjWebProgramming.Migrations
{
    public partial class directorImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Directors",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Directors");
        }
    }
}
