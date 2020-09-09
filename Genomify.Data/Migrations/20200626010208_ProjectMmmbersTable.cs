using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class ProjectMmmbersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projectMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorTestUserId = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectMembers_AspNetUsers_AuthorTestUserId",
                        column: x => x.AuthorTestUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_projectMembers_ProjectInfo_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projectMembers_AuthorTestUserId",
                table: "projectMembers",
                column: "AuthorTestUserId");

            migrationBuilder.CreateIndex(
                name: "IX_projectMembers_ProjectId",
                table: "projectMembers",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projectMembers");
        }
    }
}
