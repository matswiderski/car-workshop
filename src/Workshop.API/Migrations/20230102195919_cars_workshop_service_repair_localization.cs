using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshop.API.Migrations
{
    public partial class cars_workshop_service_repair_localization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localization_BusinessUsers_BusinessUserId",
                table: "Localization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localization",
                table: "Localization");

            migrationBuilder.DropIndex(
                name: "IX_Localization_BusinessUserId",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BusinessUsers");

            migrationBuilder.DropColumn(
                name: "BusinessUserId",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Localization");

            migrationBuilder.RenameTable(
                name: "Localization",
                newName: "Localizations");

            migrationBuilder.RenameColumn(
                name: "OwnerLastName",
                table: "BusinessUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "OwnerFirstName",
                table: "BusinessUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "WorkshopId",
                table: "Localizations",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalizationId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workshops_BusinessUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonalUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkshopId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_PersonalUsers_PersonalUserId",
                        column: x => x.PersonalUserId,
                        principalTable: "PersonalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Repairs_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopServices",
                columns: table => new
                {
                    WorkshopId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopServices", x => new { x.ServiceId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_WorkshopServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopServices_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairServices",
                columns: table => new
                {
                    RepairId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairServices", x => new { x.ServiceId, x.RepairId });
                    table.ForeignKey(
                        name: "FK_RepairServices_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { "1d16eea6-efc0-4cf9-bdba-b69326495071", "Repair", "Engine repair", 2000.0 },
                    { "1e256131-463d-4412-8759-37d1c640420b", "Maintenance", "Brake maintenance", 100.0 },
                    { "2e264c35-29ce-40f2-9377-cb7a6ac9fb88", "Air conditioning", "Evaporator repair", 100.0 },
                    { "32b98306-a9b0-4672-8203-bd8ad14fcdf7", "Detailing", "Exterior wash", 200.0 },
                    { "33625433-e450-4100-946c-1a8ccd8305ca", "Repair", "Transmission repair", 1000.0 },
                    { "53c816fa-792f-4e2f-b35d-33a2e43f5df0", "Safety inspections", "Safety inspection", 300.0 },
                    { "6182c48f-4e41-4609-808e-f6df95e5d85f", "Maintenance", "Oil change", 201.5 },
                    { "7f785c6b-99d2-41d3-aeea-1d54dbc7c964", "Electrical system", "Repair lectrical issues", 200.0 },
                    { "8f30b948-9c82-4448-90f0-e0b93dae57e8", "Air conditioning", "Compressor repair", 100.0 },
                    { "9bcc9fd5-0a5a-439d-b76a-4acc39714d60", "Maintenance", "Tire rotation", 50.0 },
                    { "ba654a63-7f40-4890-82f6-bed5037392fc", "Repair", "Suspension repair", 500.0 },
                    { "bb8281ea-0583-4705-b8c7-9da2692d9542", "Other", "Other", 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_WorkshopId",
                table: "Localizations",
                column: "WorkshopId",
                unique: true,
                filter: "[WorkshopId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonalUserId",
                table: "Cars",
                column: "PersonalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CarId",
                table: "Repairs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_PersonalUserId",
                table: "Repairs",
                column: "PersonalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_WorkshopId",
                table: "Repairs",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairServices_RepairId",
                table: "RepairServices",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_OwnerId",
                table: "Workshops",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServices_WorkshopId",
                table: "WorkshopServices",
                column: "WorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_Workshops_WorkshopId",
                table: "Localizations",
                column: "WorkshopId",
                principalTable: "Workshops",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_Workshops_WorkshopId",
                table: "Localizations");

            migrationBuilder.DropTable(
                name: "RepairServices");

            migrationBuilder.DropTable(
                name: "WorkshopServices");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizations",
                table: "Localizations");

            migrationBuilder.DropIndex(
                name: "IX_Localizations_WorkshopId",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "WorkshopId",
                table: "Localizations");

            migrationBuilder.RenameTable(
                name: "Localizations",
                newName: "Localization");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "BusinessUsers",
                newName: "OwnerLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "BusinessUsers",
                newName: "OwnerFirstName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BusinessUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessUserId",
                table: "Localization",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Localization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localization",
                table: "Localization",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Localization_BusinessUserId",
                table: "Localization",
                column: "BusinessUserId",
                unique: true);

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
