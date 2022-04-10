using Microsoft.EntityFrameworkCore.Migrations;

namespace LecturesAttendanceSystem.Data.Migrations
{
    public partial class AddedLessonTypeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonType",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonType",
                table: "Lessons");
        }
    }
}
