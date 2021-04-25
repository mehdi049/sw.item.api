using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.Item.Data.Migrations
{
    public partial class Init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateTime",
                table: "Item",
                newName: "LastUpdatedTime");
            
            migrationBuilder.AddColumn<int>(
                name: "Seen",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LikedItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedItem");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedTime",
                table: "Item",
                newName: "LastUpdateTime");

        }
    }
}
