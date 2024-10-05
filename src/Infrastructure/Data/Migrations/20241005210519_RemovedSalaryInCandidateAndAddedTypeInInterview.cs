using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkaHR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSalaryInCandidateAndAddedTypeInInterview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryPreference",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Interviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Interviews");

            migrationBuilder.AddColumn<int>(
                name: "SalaryPreference",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
