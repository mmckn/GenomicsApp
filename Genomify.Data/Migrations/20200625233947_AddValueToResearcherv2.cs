using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddValueToResearcherv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentdNumberOfParticipants",
                table: "ProjectInfo",
                newName: "CurrentNumberOfParticipants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentNumberOfParticipants",
                table: "ProjectInfo",
                newName: "CurrentdNumberOfParticipants");
        }
    }
}
