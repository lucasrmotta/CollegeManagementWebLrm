using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeManagement.Migrations
{
    public partial class FixPKStudentGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_STUDENT_SUBJECT",
                table: "STUDENT_GRADES",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUDENT_GRADES",
                table: "STUDENT_GRADES",
                column: "ID_STUDENT_SUBJECT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_STUDENT_GRADES",
                table: "STUDENT_GRADES");

            migrationBuilder.DropColumn(
                name: "ID_STUDENT_SUBJECT",
                table: "STUDENT_GRADES");
        }
    }
}
