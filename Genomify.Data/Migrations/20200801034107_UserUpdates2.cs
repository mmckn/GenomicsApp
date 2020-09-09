using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class UserUpdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectUpdate",
                table: "projectMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectUpdate",
                table: "projectMembers");
        }
    }
}
