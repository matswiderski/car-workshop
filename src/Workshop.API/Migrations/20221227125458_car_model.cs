using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshop.API.Migrations
{
    public partial class car_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localization_BusinessUsers_BusinessUserId",
                table: "Localization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localization",
                table: "Localization");

            migrationBuilder.RenameTable(
                name: "Localization",
                newName: "Localizations");

            migrationBuilder.RenameIndex(
                name: "IX_Localization_BusinessUserId",
                table: "Localizations",
                newName: "IX_Localizations_BusinessUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizations",
                table: "Localizations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    PersonalUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_PersonalUsers_PersonalUserId",
                        column: x => x.PersonalUserId,
                        principalTable: "PersonalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonalUserId",
                table: "Cars",
                column: "PersonalUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_BusinessUsers_BusinessUserId",
                table: "Localizations",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_BusinessUsers_BusinessUserId",
                table: "Localizations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizations",
                table: "Localizations");

            migrationBuilder.RenameTable(
                name: "Localizations",
                newName: "Localization");

            migrationBuilder.RenameIndex(
                name: "IX_Localizations_BusinessUserId",
                table: "Localization",
                newName: "IX_Localization_BusinessUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localization",
                table: "Localization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Localization_BusinessUsers_BusinessUserId",
                table: "Localization",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
