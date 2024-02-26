using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCodersWebApi.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseDescription", "CourseName", "CourseThumbnail", "IsRequire", "Rank" },
                values: new object[] { 1, "This course is a react course", "React", "", true, 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseDescription", "CourseName", "CourseThumbnail", "IsRequire", "Rank" },
                values: new object[] { 2, "This course is a flutter course", "Flutter", "", false, 1 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseDescription", "CourseName", "CourseThumbnail", "IsRequire", "Rank" },
                values: new object[] { 3, "This course is a bootstrap 5 course", "Bootstrap", "", false, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
