using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AuthorTest.Migrations
{
    public partial class AddedTableForResearcherDetailsRemovedIdenitityUserextension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "ResearcherInfo");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ResearcherInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "ResearcherInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ResearcherInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "ResearcherInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "ResearcherInfo",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "ResearcherInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "ResearcherInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ResearcherInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
