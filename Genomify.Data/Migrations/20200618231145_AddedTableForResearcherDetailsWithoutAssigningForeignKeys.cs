using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddedTableForResearcherDetailsWithoutAssigningForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ResearcherInfo_ResearcherId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResearcherId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthorTestUserId",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "ResearcherId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorTestUserId",
                table: "ResearcherInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResearcherId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ResearcherId",
                table: "AspNetUsers",
                column: "ResearcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ResearcherInfo_ResearcherId",
                table: "AspNetUsers",
                column: "ResearcherId",
                principalTable: "ResearcherInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
