using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class ChangedUserValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchOrganization",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResearcherRole",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ResearchOrganizationAddress",
                table: "ResearcherInfo",
                type: "nvarchar(90)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResearchOrganizationEmail",
                table: "ResearcherInfo",
                type: "nvarchar(90)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchOrganizationAddress",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "ResearchOrganizationEmail",
                table: "ResearcherInfo");

            migrationBuilder.AddColumn<string>(
                name: "ResearchOrganization",
                table: "AspNetUsers",
                type: "nvarchar(90)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResearcherRole",
                table: "AspNetUsers",
                type: "nvarchar(90)",
                nullable: true);
        }
    }
}
