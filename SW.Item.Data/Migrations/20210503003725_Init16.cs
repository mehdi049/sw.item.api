using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemStatusId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemStatusId",
                table: "Item",
                column: "ItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemStatus_ItemStatusId",
                table: "Item",
                column: "ItemStatusId",
                principalTable: "ItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemStatus_ItemStatusId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "ItemStatus");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemStatusId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemStatusId",
                table: "Item");
        }
    }
}
