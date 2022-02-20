using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    ID_COURSE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DS_COURSE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSES", x => x.ID_COURSE);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    ID_TEACHER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "date", nullable: false),
                    SALARY = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.ID_TEACHER);
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    ID_STUDENT_REGISTRATION_NUMBER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "date", nullable: false),
                    ID_COURSE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.ID_STUDENT_REGISTRATION_NUMBER);
                    table.ForeignKey(
                        name: "FK_STUDENTS_COURSES_ID_COURSE",
                        column: x => x.ID_COURSE,
                        principalTable: "COURSES",
                        principalColumn: "ID_COURSE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SUBJECTS",
                columns: table => new
                {
                    ID_SUBJECT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DS_SUBJECT = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ID_TEACHER = table.Column<int>(type: "int", nullable: false),
                    ID_COURSE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECTS", x => x.ID_SUBJECT);
                    table.ForeignKey(
                        name: "FK_SUBJECTS_COURSES_ID_COURSE",
                        column: x => x.ID_COURSE,
                        principalTable: "COURSES",
                        principalColumn: "ID_COURSE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SUBJECTS_Teachers_ID_TEACHER",
                        column: x => x.ID_TEACHER,
                        principalTable: "Teachers",
                        principalColumn: "ID_TEACHER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_GRADES",
                columns: table => new
                {
                    ID__STUDENT_REGISTRATION_NUMBER = table.Column<int>(type: "int", nullable: false),
                    ID_SUBJECT = table.Column<int>(type: "int", nullable: false),
                    GRADE = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_STUDENT_GRADES_STUDENTS_ID__STUDENT_REGISTRATION_NUMBER",
                        column: x => x.ID__STUDENT_REGISTRATION_NUMBER,
                        principalTable: "STUDENTS",
                        principalColumn: "ID_STUDENT_REGISTRATION_NUMBER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_GRADES_SUBJECTS_ID_SUBJECT",
                        column: x => x.ID_SUBJECT,
                        principalTable: "SUBJECTS",
                        principalColumn: "ID_SUBJECT",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_GRADES_ID__STUDENT_REGISTRATION_NUMBER",
                table: "STUDENT_GRADES",
                column: "ID__STUDENT_REGISTRATION_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_GRADES_ID_SUBJECT",
                table: "STUDENT_GRADES",
                column: "ID_SUBJECT");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_ID_COURSE",
                table: "STUDENTS",
                column: "ID_COURSE");

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECTS_ID_COURSE",
                table: "SUBJECTS",
                column: "ID_COURSE");

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECTS_ID_TEACHER",
                table: "SUBJECTS",
                column: "ID_TEACHER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENT_GRADES");

            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "SUBJECTS");

            migrationBuilder.DropTable(
                name: "COURSES");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
