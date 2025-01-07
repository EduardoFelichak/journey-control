using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace journey_control.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_version",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_version", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_versions", x => new { x.Id, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_versions_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Project = table.Column<int>(type: "integer", nullable: false),
                    VersionId = table.Column<int>(type: "integer", nullable: false),
                    VersionProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_tasks_versions_VersionId_VersionProjectId",
                        columns: x => new { x.VersionId, x.VersionProjectId },
                        principalTable: "versions",
                        principalColumns: new[] { "Id", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entries",
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
                    table.PrimaryKey("PK_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entries_tasks_TaskId_TaskUserId",
                        columns: x => new { x.TaskId, x.TaskUserId },
                        principalTable: "tasks",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "local_entries",
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
                    table.PrimaryKey("PK_local_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_local_entries_tasks_TaskId_TaskUserId",
                        columns: x => new { x.TaskId, x.TaskUserId },
                        principalTable: "tasks",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entries_TaskId_TaskUserId",
                table: "entries",
                columns: new[] { "TaskId", "TaskUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_local_entries_TaskId_TaskUserId",
                table: "local_entries",
                columns: new[] { "TaskId", "TaskUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_tasks_VersionId_VersionProjectId",
                table: "tasks",
                columns: new[] { "VersionId", "VersionProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_versions_ProjectId",
                table: "versions",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_version");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "local_entries");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "versions");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
