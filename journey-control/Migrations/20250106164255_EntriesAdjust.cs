using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace journey_control.Migrations
{
    /// <inheritdoc />
    public partial class EntriesAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInProgress",
                table: "entries");

            migrationBuilder.CreateTable(
                name: "LocalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<string>(type: "text", nullable: false),
                    TaskUserId = table.Column<int>(type: "integer", nullable: false),
                    DateEntrie = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalEntries_tasks_TaskId_TaskUserId",
                        columns: x => new { x.TaskId, x.TaskUserId },
                        principalTable: "tasks",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalEntries_TaskId_TaskUserId",
                table: "LocalEntries",
                columns: new[] { "TaskId", "TaskUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalEntries");

            migrationBuilder.AddColumn<bool>(
                name: "IsInProgress",
                table: "entries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
