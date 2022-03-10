using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Businesses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Businesses",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Businesses");
        }
    }
}
