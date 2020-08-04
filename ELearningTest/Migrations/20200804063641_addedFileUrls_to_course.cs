using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELearningTest.Migrations
{
    public partial class addedFileUrls_to_course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "UserCourses",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "FileUrls",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "FileUrls",
                table: "Courses");
        }
    }
}
