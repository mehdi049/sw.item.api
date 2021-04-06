using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedTime",
                table: "Item",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Item",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemFeedback_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemFeedback_ItemId",
                table: "ItemFeedback",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemFeedback");

            migrationBuilder.DropColumn(
                name: "AddedTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Item");
        }
    }
}
