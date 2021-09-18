using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentCoursesManagement.Infrastructure.Migrations
{
    public partial class SeedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Students (Name, StudentNo) VALUES ('Student 1', '1234567')");
            migrationBuilder.Sql("INSERT INTO Students (Name, StudentNo) VALUES ('Student 2', '2134567')");
            migrationBuilder.Sql("INSERT INTO Students (Name, StudentNo) VALUES ('Student 3', '3214567')");
            migrationBuilder.Sql("INSERT INTO Students (Name, StudentNo) VALUES ('Student 4', '4231567')");
            migrationBuilder.Sql("INSERT INTO Students (Name, StudentNo) VALUES ('Student 5', '5234167')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Students WHERE Name IN ('Student 1', 'Student 2', 'Student 3', 'Student 4', 'Student 5')");
        }
    }
}
