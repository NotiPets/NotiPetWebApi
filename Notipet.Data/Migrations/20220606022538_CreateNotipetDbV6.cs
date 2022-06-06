using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DigitalVaccines_Businesses_BusinessId",
                table: "DigitalVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_DigitalVaccines_Pets_PetId",
                table: "DigitalVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_DigitalVaccines_Users_UserId",
                table: "DigitalVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccine_Businesses_BusinessId",
                table: "Vaccine");

            migrationBuilder.DropIndex(
                name: "IX_Vaccine_BusinessId",
                table: "Vaccine");

            migrationBuilder.DropIndex(
                name: "IX_DigitalVaccines_BusinessId",
                table: "DigitalVaccines");

            migrationBuilder.DropIndex(
                name: "IX_DigitalVaccines_PetId",
                table: "DigitalVaccines");

            migrationBuilder.DropIndex(
                name: "IX_DigitalVaccines_UserId",
                table: "DigitalVaccines");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "AssetsServices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "AssetsServices");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_BusinessId",
                table: "Vaccine",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalVaccines_BusinessId",
                table: "DigitalVaccines",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalVaccines_PetId",
                table: "DigitalVaccines",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalVaccines_UserId",
                table: "DigitalVaccines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DigitalVaccines_Businesses_BusinessId",
                table: "DigitalVaccines",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DigitalVaccines_Pets_PetId",
                table: "DigitalVaccines",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DigitalVaccines_Users_UserId",
                table: "DigitalVaccines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccine_Businesses_BusinessId",
                table: "Vaccine",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
