using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "DigitalVaccines");

            migrationBuilder.AddColumn<Guid>(
                name: "VaccineId",
                table: "DigitalVaccines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PetTypeId = table.Column<int>(type: "integer", nullable: false),
                    VaccineName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BusinessId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccine_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vaccine_PetType_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DigitalVaccines_VaccineId",
                table: "DigitalVaccines",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_BusinessId",
                table: "Vaccine",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_PetTypeId",
                table: "Vaccine",
                column: "PetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DigitalVaccines_Vaccine_VaccineId",
                table: "DigitalVaccines",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DigitalVaccines_Vaccine_VaccineId",
                table: "DigitalVaccines");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropIndex(
                name: "IX_DigitalVaccines_VaccineId",
                table: "DigitalVaccines");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "DigitalVaccines");

            migrationBuilder.AddColumn<string>(
                name: "VaccineName",
                table: "DigitalVaccines",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
