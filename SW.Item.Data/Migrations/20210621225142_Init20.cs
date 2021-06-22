using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemExchanges_ExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges");

            migrationBuilder.DropTable(
                name: "ExchangeStatus");

            migrationBuilder.AddColumn<int>(
                name: "ItemExchangesId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemExchangeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemExchangeStatus", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ItemExchanges_ItemExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges",
                column: "ExchangeStatusId",
                principalTable: "ItemExchangeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemExchanges_ItemExchangesId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemExchanges_ItemExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges");

            migrationBuilder.DropTable(
                name: "ItemExchangeStatus");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemExchangesId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemExchangesId",
                table: "Item");

            migrationBuilder.CreateTable(
                name: "ExchangeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeStatus", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemExchanges_ExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges",
                column: "ExchangeStatusId",
                principalTable: "ExchangeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
