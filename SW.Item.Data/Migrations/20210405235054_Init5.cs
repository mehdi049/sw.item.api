using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCondition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ConditionId",
                table: "Item",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemCondition_ConditionId",
                table: "Item",
                column: "ConditionId",
                principalTable: "ItemCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemCondition_ConditionId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "ItemCondition");

            migrationBuilder.DropIndex(
                name: "IX_Item_ConditionId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Item",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
