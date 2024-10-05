using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MilkaHR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedInterviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJobProcessing_Candidates_CandidateId",
                table: "CandidateJobProcessing");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJobProcessing_Jobs_JobId",
                table: "CandidateJobProcessing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateJobProcessing",
                table: "CandidateJobProcessing");

            migrationBuilder.RenameTable(
                name: "CandidateJobProcessing",
                newName: "CandidateJobProcessings");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJobProcessing_JobId",
                table: "CandidateJobProcessings",
                newName: "IX_CandidateJobProcessings_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJobProcessing_CandidateId",
                table: "CandidateJobProcessings",
                newName: "IX_CandidateJobProcessings_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateJobProcessings",
                table: "CandidateJobProcessings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timing = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JobId = table.Column<int>(type: "integer", nullable: false),
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    RecruiterId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interviews_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interviews_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_CandidateId",
                table: "Interviews",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_JobId",
                table: "Interviews",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_RecruiterId",
                table: "Interviews",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJobProcessings_Candidates_CandidateId",
                table: "CandidateJobProcessings",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJobProcessings_Jobs_JobId",
                table: "CandidateJobProcessings",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJobProcessings_Candidates_CandidateId",
                table: "CandidateJobProcessings");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJobProcessings_Jobs_JobId",
                table: "CandidateJobProcessings");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateJobProcessings",
                table: "CandidateJobProcessings");

            migrationBuilder.RenameTable(
                name: "CandidateJobProcessings",
                newName: "CandidateJobProcessing");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJobProcessings_JobId",
                table: "CandidateJobProcessing",
                newName: "IX_CandidateJobProcessing_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJobProcessings_CandidateId",
                table: "CandidateJobProcessing",
                newName: "IX_CandidateJobProcessing_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateJobProcessing",
                table: "CandidateJobProcessing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJobProcessing_Candidates_CandidateId",
                table: "CandidateJobProcessing",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJobProcessing_Jobs_JobId",
                table: "CandidateJobProcessing",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
