using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class ChangedProjectTableValuesv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchOrganization",
                table: "ProjectInfo");

            migrationBuilder.RenameColumn(
                name: "ResearchOrganizationEmail",
                table: "ProjectInfo",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ResearchOrganizationAddress",
                table: "ProjectInfo",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numberOfParticipants",
                table: "ProjectInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectInfo");

            migrationBuilder.DropColumn(
                name: "numberOfParticipants",
                table: "ProjectInfo");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProjectInfo",
                newName: "ResearchOrganizationEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ProjectInfo",
                newName: "ResearchOrganizationAddress");

            migrationBuilder.AddColumn<string>(
                name: "ResearchOrganization",
                table: "ProjectInfo",
                type: "nvarchar(90)",
                nullable: true);
        }
    }
}
