using Microsoft.EntityFrameworkCore.Migrations;

namespace MayMeow.RPG.Data.Migrations
{
    public partial class AddPlayableOptionToCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Playable",
                table: "Characters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Playable",
                table: "Characters");
        }
    }
}
