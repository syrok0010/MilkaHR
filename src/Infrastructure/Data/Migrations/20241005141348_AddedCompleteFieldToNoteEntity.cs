using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkaHR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompleteFieldToNoteEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Notes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Notes");
        }
    }
}
