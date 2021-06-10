using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Api.Migrations
{
    public partial class CreateUsersDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Internal id (only used for joins, etc.).")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "First name of the user."),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Last name of the user.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
