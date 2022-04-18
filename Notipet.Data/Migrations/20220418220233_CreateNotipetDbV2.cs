using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AssetsServices_AssetsServicesId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AssetsServicesId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AssetsServicesId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Ratings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Businesses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_SpecialityId",
                table: "Specialists",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssetsServicesId",
                table: "Orders",
                column: "AssetsServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialistId",
                table: "Appointments",
                column: "SpecialistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialists_SpecialistId",
                table: "Appointments",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AssetsServices_AssetsServicesId",
                table: "Orders",
                column: "AssetsServicesId",
                principalTable: "AssetsServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Specialities_SpecialityId",
                table: "Specialists",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialists_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AssetsServices_AssetsServicesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Specialities_SpecialityId",
                table: "Specialists");

            migrationBuilder.DropIndex(
                name: "IX_Specialists_SpecialityId",
                table: "Specialists");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssetsServicesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Businesses");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Ratings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "AssetsServicesId",
                table: "Ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AssetsServicesId",
                table: "Ratings",
                column: "AssetsServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AssetsServices_AssetsServicesId",
                table: "Ratings",
                column: "AssetsServicesId",
                principalTable: "AssetsServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }
    }
}
