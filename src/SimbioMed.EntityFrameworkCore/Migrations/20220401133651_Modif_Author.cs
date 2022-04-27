using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbioMed.Migrations
{
    public partial class Modif_Author : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorPhoto",
                table: "Author");

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileBytes",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Author",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFileBytes",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Author");

            migrationBuilder.AddColumn<byte[]>(
                name: "AuthorPhoto",
                table: "Author",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
