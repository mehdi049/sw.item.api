using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemExchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemExchanges_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemExchanges_ItemId",
                table: "ItemExchanges",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemExchanges");
        }
    }
}
