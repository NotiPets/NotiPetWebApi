using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Businesses_BusinessId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_BusinessId",
                table: "UserRoles");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Businesses",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Businesses");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_BusinessId",
                table: "UserRoles",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Businesses_BusinessId",
                table: "UserRoles",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
