using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "AssetsServices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "AssetsServices");
        }
    }
}
