using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Pets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Completed" },
                    { 4, "Denied" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Pets",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
