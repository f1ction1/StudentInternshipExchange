using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Practices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class internship_status_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InternshipStatus",
                schema: "interships",
                table: "Internships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternshipStatus",
                schema: "interships",
                table: "Internships");
        }
    }
}
