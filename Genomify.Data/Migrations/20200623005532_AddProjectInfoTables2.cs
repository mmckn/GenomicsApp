using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorTest.Migrations
{
    public partial class AddProjectInfoTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    ResearchOrganization = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    ResearchOrganizationAddress = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    ResearchOrganizationEmail = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    ResearcherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_ResearcherInfo_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "ResearcherInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_ResearcherId",
                table: "ProjectInfo",
                column: "ResearcherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectInfo");
        }
    }
}
