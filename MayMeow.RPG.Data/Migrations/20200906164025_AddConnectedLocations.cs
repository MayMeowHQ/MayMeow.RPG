using Microsoft.EntityFrameworkCore.Migrations;

namespace MayMeow.RPG.Data.Migrations
{
    public partial class AddConnectedLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectedLocations",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedLocations", x => new { x.ChildId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_ConnectedLocations_Locations_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConnectedLocations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedLocations_ParentId",
                table: "ConnectedLocations",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectedLocations");
        }
    }
}
