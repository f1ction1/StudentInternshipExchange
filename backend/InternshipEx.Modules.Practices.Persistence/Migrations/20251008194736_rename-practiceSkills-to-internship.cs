using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Practices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renamepracticeSkillstointernship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracticeSkills_Internships_InternshipsId",
                schema: "interships",
                table: "PracticeSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticeSkills_Skills_SkillsId",
                schema: "interships",
                table: "PracticeSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PracticeSkills",
                schema: "interships",
                table: "PracticeSkills");

            migrationBuilder.RenameTable(
                name: "PracticeSkills",
                schema: "interships",
                newName: "InternshipSkills",
                newSchema: "interships");

            migrationBuilder.RenameIndex(
                name: "IX_PracticeSkills_SkillsId",
                schema: "interships",
                table: "InternshipSkills",
                newName: "IX_InternshipSkills_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipSkills",
                schema: "interships",
                table: "InternshipSkills",
                columns: new[] { "InternshipsId", "SkillsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipSkills_Internships_InternshipsId",
                schema: "interships",
                table: "InternshipSkills",
                column: "InternshipsId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipSkills_Skills_SkillsId",
                schema: "interships",
                table: "InternshipSkills",
                column: "SkillsId",
                principalSchema: "interships",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipSkills_Internships_InternshipsId",
                schema: "interships",
                table: "InternshipSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipSkills_Skills_SkillsId",
                schema: "interships",
                table: "InternshipSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipSkills",
                schema: "interships",
                table: "InternshipSkills");

            migrationBuilder.RenameTable(
                name: "InternshipSkills",
                schema: "interships",
                newName: "PracticeSkills",
                newSchema: "interships");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipSkills_SkillsId",
                schema: "interships",
                table: "PracticeSkills",
                newName: "IX_PracticeSkills_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PracticeSkills",
                schema: "interships",
                table: "PracticeSkills",
                columns: new[] { "InternshipsId", "SkillsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeSkills_Internships_InternshipsId",
                schema: "interships",
                table: "PracticeSkills",
                column: "InternshipsId",
                principalSchema: "interships",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeSkills_Skills_SkillsId",
                schema: "interships",
                table: "PracticeSkills",
                column: "SkillsId",
                principalSchema: "interships",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
