using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkaHR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdditionalInfoToCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Candidates",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastJob",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Tags",
                table: "Candidates",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkExperience",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LastJob",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "WorkExperience",
                table: "Candidates");
        }
    }
}
