using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEx.Modules.Practices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cityadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                schema: "interships",
                table: "Internships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "interships",
                table: "Internships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Internships_CountryId",
                schema: "interships",
                table: "Internships",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_Countries_CountryId",
                schema: "interships",
                table: "Internships",
                column: "CountryId",
                principalSchema: "interships",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Countries_CountryId",
                schema: "interships",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_CountryId",
                schema: "interships",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "interships",
                table: "Internships");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                schema: "interships",
                table: "Internships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
