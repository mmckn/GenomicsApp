using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddedTableForResearcherDetailswithForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorTestUserId",
                table: "ResearcherInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherInfo_AuthorTestUserId",
                table: "ResearcherInfo",
                column: "AuthorTestUserId",
                unique: true,
                filter: "[AuthorTestUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ResearcherInfo_AspNetUsers_AuthorTestUserId",
                table: "ResearcherInfo",
                column: "AuthorTestUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearcherInfo_AspNetUsers_AuthorTestUserId",
                table: "ResearcherInfo");

            migrationBuilder.DropIndex(
                name: "IX_ResearcherInfo_AuthorTestUserId",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "AuthorTestUserId",
                table: "ResearcherInfo");
        }
    }
}
