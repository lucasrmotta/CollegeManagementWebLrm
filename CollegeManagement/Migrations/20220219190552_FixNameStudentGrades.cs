using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeManagement.Migrations
{
    public partial class FixNameStudentGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STUDENT_GRADES_STUDENTS_ID__STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES");

            migrationBuilder.RenameColumn(
                name: "ID__STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                newName: "ID_STUDENT_REGISTRATION_NUMBER");

            migrationBuilder.RenameIndex(
                name: "IX_STUDENT_GRADES_ID__STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                newName: "IX_STUDENT_GRADES_ID_STUDENT_REGISTRATION_NUMBER");

            migrationBuilder.AddForeignKey(
                name: "FK_STUDENT_GRADES_STUDENTS_ID_STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                column: "ID_STUDENT_REGISTRATION_NUMBER",
                principalTable: "STUDENTS",
                principalColumn: "ID_STUDENT_REGISTRATION_NUMBER",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STUDENT_GRADES_STUDENTS_ID_STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES");

            migrationBuilder.RenameColumn(
                name: "ID_STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                newName: "ID__STUDENT_REGISTRATION_NUMBER");

            migrationBuilder.RenameIndex(
                name: "IX_STUDENT_GRADES_ID_STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                newName: "IX_STUDENT_GRADES_ID__STUDENT_REGISTRATION_NUMBER");

            migrationBuilder.AddForeignKey(
                name: "FK_STUDENT_GRADES_STUDENTS_ID__STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                column: "ID__STUDENT_REGISTRATION_NUMBER",
                principalTable: "STUDENTS",
                principalColumn: "ID_STUDENT_REGISTRATION_NUMBER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
