using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntershipEx.Modules.Applications.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class INternshipInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                schema: "applications",
                table: "Applications",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "InternshipInfo_CompanyLocation",
                schema: "applications",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternshipInfo_CompanyName",
                schema: "applications",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternshipInfo_Title",
                schema: "applications",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternshipInfo_CompanyLocation",
                schema: "applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InternshipInfo_CompanyName",
                schema: "applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InternshipInfo_Title",
                schema: "applications",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "applications",
                table: "Applications",
                newName: "AppliedAt");
        }
    }
}
