using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LecturesAttendanceSystem.Data.Migrations
{
    public partial class Addeduserfieldregisteredon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "Users");
        }
    }
}
