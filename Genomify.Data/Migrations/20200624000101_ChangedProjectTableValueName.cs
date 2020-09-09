using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class ChangedProjectTableValueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "ProjectInfo");

            migrationBuilder.RenameColumn(
                name: "numberOfParticipants",
                table: "ProjectInfo",
                newName: "NumberOfParticipants");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProjectInfo",
                newName: "ProjectTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfParticipants",
                table: "ProjectInfo",
                newName: "numberOfParticipants");

            migrationBuilder.RenameColumn(
                name: "ProjectTitle",
                table: "ProjectInfo",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "ProjectInfo",
                type: "nvarchar(90)",
                nullable: true);
        }
    }
}
