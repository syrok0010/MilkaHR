﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkaHR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryToJobEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Jobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Jobs");
        }
    }
}
