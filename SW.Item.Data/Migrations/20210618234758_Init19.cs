using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ItemExchanges");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeStatusId",
                table: "ItemExchanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ItemExchanges_ExchangeStatusId",
                table: "ItemExchanges",
                column: "ExchangeStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemExchanges_ExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges",
                column: "ExchangeStatusId",
                principalTable: "ExchangeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemExchanges_ExchangeStatus_ExchangeStatusId",
                table: "ItemExchanges");

            migrationBuilder.DropTable(
                name: "ExchangeStatus");

            migrationBuilder.DropIndex(
                name: "IX_ItemExchanges_ExchangeStatusId",
                table: "ItemExchanges");

            migrationBuilder.DropColumn(
                name: "ExchangeStatusId",
                table: "ItemExchanges");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ItemExchanges",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
