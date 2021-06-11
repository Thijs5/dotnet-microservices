using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeEntries.Api.Migrations
{
    public partial class CreateTimeEntriesDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique id of the entry.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Id of the linked project."),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "Id of the linked user."),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp from."),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp until.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}
