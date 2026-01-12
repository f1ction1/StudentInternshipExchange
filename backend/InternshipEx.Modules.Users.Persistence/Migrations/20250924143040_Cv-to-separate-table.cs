using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Cvtoseparatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFile",
                schema: "users",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "CvId",
                schema: "users",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cvs",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CvFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cvs_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "users",
                        principalTable: "Students",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_StudentId",
                schema: "users",
                table: "Cvs",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cvs",
                schema: "users");

            migrationBuilder.DropColumn(
                name: "CvId",
                schema: "users",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "CvFile",
                schema: "users",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
