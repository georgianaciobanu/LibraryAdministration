using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbioMed.Migrations
{
    public partial class add_city_to_store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_StoreId",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Store",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Store_StoreId",
                table: "Store",
                newName: "IX_Store_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_CityId",
                table: "Store",
                column: "CityId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_CityId",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Store",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Store_CityId",
                table: "Store",
                newName: "IX_Store_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_StoreId",
                table: "Store",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
