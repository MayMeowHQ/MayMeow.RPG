using Microsoft.EntityFrameworkCore.Migrations;

namespace MayMeow.RPG.Data.Migrations
{
    public partial class AddActiveCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Characters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Characters");
        }
    }
}
