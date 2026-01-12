using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Practices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationToSeparateService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipFavorites_Internships_PracticeId",
                schema: "interships",
                table: "InternshipFavorites");

            migrationBuilder.DropTable(
                name: "ApplicationStatusHistories",
                schema: "interships");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "interships");

            migrationBuilder.RenameColumn(
                name: "PracticeId",
                schema: "interships",
                table: "InternshipFavorites",
                newName: "InternshipId");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationsCount",
                schema: "interships",
                table: "Internships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipFavorites_Internships_InternshipId",
                schema: "interships",
                table: "InternshipFavorites",
                column: "InternshipId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipFavorites_Internships_InternshipId",
                schema: "interships",
                table: "InternshipFavorites");

            migrationBuilder.DropColumn(
                name: "ApplicationsCount",
                schema: "interships",
                table: "Internships");

            migrationBuilder.RenameColumn(
                name: "InternshipId",
                schema: "interships",
                table: "InternshipFavorites",
                newName: "PracticeId");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "interships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Internships_PracticeId",
                        column: x => x.PracticeId,
                        principalSchema: "interships",
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatusHistories",
                schema: "interships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationStatusHistories_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "interships",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PracticeId",
                schema: "interships",
                table: "Applications",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatusHistories_ApplicationId",
                schema: "interships",
                table: "ApplicationStatusHistories",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipFavorites_Internships_PracticeId",
                schema: "interships",
                table: "InternshipFavorites",
                column: "PracticeId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
