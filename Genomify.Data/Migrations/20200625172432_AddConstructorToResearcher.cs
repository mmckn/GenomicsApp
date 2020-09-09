using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddConstructorToResearcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfParticipants",
                table: "ProjectInfo",
                newName: "RequiredNumberOfParticipants");

            migrationBuilder.AddColumn<string>(
                name: "ResearchOrganization",
                table: "ProjectInfo",
                type: "nvarchar(90)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchOrganization",
                table: "ProjectInfo");

            migrationBuilder.RenameColumn(
                name: "RequiredNumberOfParticipants",
                table: "ProjectInfo",
                newName: "NumberOfParticipants");
        }
    }
}
