using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notipet.Data.Migrations
{
    public partial class CreateNotipetDbV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialist_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Specialist_SpecialistId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialist_Speciality_SpecialityId",
                table: "Specialist");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialist_Users_UserId",
                table: "Specialist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialist",
                table: "Specialist");

            migrationBuilder.RenameTable(
                name: "Speciality",
                newName: "Specialities");

            migrationBuilder.RenameTable(
                name: "Specialist",
                newName: "Specialists");

            migrationBuilder.RenameIndex(
                name: "IX_Specialist_UserId",
                table: "Specialists",
                newName: "IX_Specialists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialist_SpecialityId",
                table: "Specialists",
                newName: "IX_Specialists_SpecialityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialists",
                table: "Specialists",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Small" },
                    { 1, "Medium" },
                    { 2, "Large" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SizeId",
                table: "Pets",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialists_SpecialistId",
                table: "Appointments",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Size_SizeId",
                table: "Pets",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Specialists_SpecialistId",
                table: "Ratings",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Specialities_SpecialityId",
                table: "Specialists",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Users_UserId",
                table: "Specialists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialists_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Size_SizeId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Specialists_SpecialistId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Specialities_SpecialityId",
                table: "Specialists");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Users_UserId",
                table: "Specialists");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropIndex(
                name: "IX_Pets_SizeId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialists",
                table: "Specialists");

            migrationBuilder.RenameTable(
                name: "Specialities",
                newName: "Speciality");

            migrationBuilder.RenameTable(
                name: "Specialists",
                newName: "Specialist");

            migrationBuilder.RenameIndex(
                name: "IX_Specialists_UserId",
                table: "Specialist",
                newName: "IX_Specialist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialists_SpecialityId",
                table: "Specialist",
                newName: "IX_Specialist_SpecialityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialist",
                table: "Specialist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialist_SpecialistId",
                table: "Appointments",
                column: "SpecialistId",
                principalTable: "Specialist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Specialist_SpecialistId",
                table: "Ratings",
                column: "SpecialistId",
                principalTable: "Specialist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialist_Speciality_SpecialityId",
                table: "Specialist",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialist_Users_UserId",
                table: "Specialist",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
