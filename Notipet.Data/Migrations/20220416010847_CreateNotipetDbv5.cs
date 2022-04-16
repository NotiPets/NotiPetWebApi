using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Specialists_SpecialistId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_SpecialistId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "SpecialistId",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Ratings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BusinessId",
                table: "Ratings",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Businesses_BusinessId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_BusinessId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Pets_UserId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialistId",
                table: "Ratings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SpecialistId",
                table: "Ratings",
                column: "SpecialistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Specialists_SpecialistId",
                table: "Ratings",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id");
        }
    }
}
