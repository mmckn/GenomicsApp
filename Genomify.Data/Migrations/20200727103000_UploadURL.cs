using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class UploadURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataUsedForProfit",
                table: "ProjectInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantRightToProfit",
                table: "ProjectInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lengthOfTimeStored",
                table: "ProjectInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataUsedForProfit",
                table: "ProjectInfo");

            migrationBuilder.DropColumn(
                name: "ParticipantRightToProfit",
                table: "ProjectInfo");

            migrationBuilder.DropColumn(
                name: "lengthOfTimeStored",
                table: "ProjectInfo");
        }
    }
}
