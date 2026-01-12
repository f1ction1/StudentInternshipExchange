using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntershipEx.Modules.Applications.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RejectFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Reject_RejectedAt",
                schema: "applications",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reject_RejectionReason",
                schema: "applications",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reject_RejectedAt",
                schema: "applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Reject_RejectionReason",
                schema: "applications",
                table: "Applications");
        }
    }
}
