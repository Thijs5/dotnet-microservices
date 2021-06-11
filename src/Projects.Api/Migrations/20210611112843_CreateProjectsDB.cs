using Microsoft.EntityFrameworkCore.Migrations;

namespace Projects.Api.Migrations
{
    public partial class CreateProjectsDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false, comment: "Unique id of the project.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table
                        .Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Projectname.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false, comment: "Internal id (only used for joins, etc.).")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "User id of the contributor."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "The id of the linked project.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributors_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ProjectId",
                table: "Contributors",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
