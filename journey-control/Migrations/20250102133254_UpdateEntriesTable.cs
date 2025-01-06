using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journey_control.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInProgress",
                table: "entries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInProgress",
                table: "entries");
        }
    }
}
