using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsServices_Businesses_BusinessId",
                table: "AssetsServices");

            migrationBuilder.DropIndex(
                name: "IX_AssetsServices_BusinessId",
                table: "AssetsServices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AssetsServices_BusinessId",
                table: "AssetsServices",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsServices_Businesses_BusinessId",
                table: "AssetsServices",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
