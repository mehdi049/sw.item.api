using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class init22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemExchanges_ItemExchangesId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemExchangesId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemExchangesId",
                table: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemExchangesId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemExchangesId",
                table: "Item",
                column: "ItemExchangesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemExchanges_ItemExchangesId",
                table: "Item",
                column: "ItemExchangesId",
                principalTable: "ItemExchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
