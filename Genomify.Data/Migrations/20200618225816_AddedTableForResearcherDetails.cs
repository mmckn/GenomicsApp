using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AuthorTest.Migrations
{
    public partial class AddedTableForResearcherDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResearcherId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResearcherInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ResearcherRole = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    ResearchOrganization = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    AuthorTestUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherInfo", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ResearcherInfo_ResearcherId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ResearcherInfo");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResearcherId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResearcherId",
                table: "AspNetUsers");
        }
    }
}
