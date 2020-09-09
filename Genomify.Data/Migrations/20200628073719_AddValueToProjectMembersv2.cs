using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddValueToProjectMembersv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Applicationstatus",
                table: "projectMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgeRange",
                table: "ProjectInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthConditions",
                table: "ProjectInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "ProjectInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Applicationstatus",
                table: "projectMembers");

            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "ProjectInfo");

            migrationBuilder.DropColumn(
                name: "HealthConditions",
                table: "ProjectInfo");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "ProjectInfo");
        }
    }
}
