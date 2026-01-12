using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Practices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renamepracticestointernship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Practices_PracticeId",
                schema: "practices",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticeSkills_Practices_PracticesId",
                schema: "practices",
                table: "PracticeSkills");

            migrationBuilder.DropTable(
                name: "PracticeFavorites",
                schema: "practices");

            migrationBuilder.DropTable(
                name: "Practices",
                schema: "practices");

            migrationBuilder.EnsureSchema(
                name: "interships");

            migrationBuilder.RenameTable(
                name: "Skills",
                schema: "practices",
                newName: "Skills",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "PracticeSkills",
                schema: "practices",
                newName: "PracticeSkills",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "Industries",
                schema: "practices",
                newName: "Industries",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "practices",
                newName: "Countries",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "practices",
                newName: "Cities",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "ApplicationStatusHistories",
                schema: "practices",
                newName: "ApplicationStatusHistories",
                newSchema: "interships");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "practices",
                newName: "Applications",
                newSchema: "interships");

            migrationBuilder.RenameColumn(
                name: "PracticesId",
                schema: "interships",
                table: "PracticeSkills",
                newName: "InternshipsId");

            migrationBuilder.CreateTable(
                name: "Internships",
                schema: "interships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    IsRemote = table.Column<bool>(type: "bit", nullable: false),
                    IndustryId = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internships_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "interships",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Internships_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalSchema: "interships",
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternshipFavorites",
                schema: "interships",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipFavorites", x => new { x.PracticeId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_InternshipFavorites_Internships_PracticeId",
                        column: x => x.PracticeId,
                        principalSchema: "interships",
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Internships_CityId",
                schema: "interships",
                table: "Internships",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_IndustryId",
                schema: "interships",
                table: "Internships",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Internships_PracticeId",
                schema: "interships",
                table: "Applications",
                column: "PracticeId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeSkills_Internships_InternshipsId",
                schema: "interships",
                table: "PracticeSkills",
                column: "InternshipsId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Internships_PracticeId",
                schema: "interships",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticeSkills_Internships_InternshipsId",
                schema: "interships",
                table: "PracticeSkills");

            migrationBuilder.DropTable(
                name: "InternshipFavorites",
                schema: "interships");

            migrationBuilder.DropTable(
                name: "Internships",
                schema: "interships");

            migrationBuilder.EnsureSchema(
                name: "practices");

            migrationBuilder.RenameTable(
                name: "Skills",
                schema: "interships",
                newName: "Skills",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "PracticeSkills",
                schema: "interships",
                newName: "PracticeSkills",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "Industries",
                schema: "interships",
                newName: "Industries",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "interships",
                newName: "Countries",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "interships",
                newName: "Cities",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "ApplicationStatusHistories",
                schema: "interships",
                newName: "ApplicationStatusHistories",
                newSchema: "practices");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "interships",
                newName: "Applications",
                newSchema: "practices");

            migrationBuilder.RenameColumn(
                name: "InternshipsId",
                schema: "practices",
                table: "PracticeSkills",
                newName: "PracticesId");

            migrationBuilder.CreateTable(
                name: "Practices",
                schema: "practices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    IndustryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployerLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsRemote = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practices_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "practices",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Practices_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalSchema: "practices",
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticeFavorites",
                schema: "practices",
                columns: table => new
                {
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeFavorites", x => new { x.PracticeId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_PracticeFavorites_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalSchema: "practices",
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Practices_CityId",
                schema: "practices",
                table: "Practices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_IndustryId",
                schema: "practices",
                table: "Practices",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Practices_PracticeId",
                schema: "practices",
                table: "Applications",
                column: "PracticeId",
                principalSchema: "practices",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeSkills_Practices_PracticesId",
                schema: "practices",
                table: "PracticeSkills",
                column: "PracticesId",
                principalSchema: "practices",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
