using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LikedItem_ItemId",
                table: "LikedItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedItem_Item_ItemId",
                table: "LikedItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedItem_Item_ItemId",
                table: "LikedItem");

            migrationBuilder.DropIndex(
                name: "IX_LikedItem_ItemId",
                table: "LikedItem");
        }
    }
}
