using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExchangeWithSubCategory",
                table: "Item",
                newName: "ExchangeWithSubCategoryId");

            migrationBuilder.RenameColumn(
                name: "ExchangeWithCategory",
                table: "Item",
                newName: "ExchangeWithCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExchangeWithSubCategoryId",
                table: "Item",
                newName: "ExchangeWithSubCategory");

            migrationBuilder.RenameColumn(
                name: "ExchangeWithCategoryId",
                table: "Item",
                newName: "ExchangeWithCategory");
        }
    }
}
