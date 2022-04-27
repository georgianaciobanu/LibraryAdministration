using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbioMed.Migrations
{
    public partial class Added_Author : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_CityId",
                table: "Store");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Cities_CityId",
                table: "Store",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Cities_CityId",
                table: "Store");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_CityId",
                table: "Store",
                column: "CityId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
