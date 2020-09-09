using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddedProjectTableValueStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProjectInfo",
                type: "nvarchar(90)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectInfo");
        }
    }
}
