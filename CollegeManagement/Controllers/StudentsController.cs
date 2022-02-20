#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollegeManagement.Data;
using CollegeManagement.Models;
using Microsoft.AspNetCore.SignalR;
using CollegeManagement.HubConfig;

namespace CollegeManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly COLLEGE_MANAGEMENT_DBContext _context;
        private readonly IHubContext<DashboardHub> _hub;

        public StudentsController(COLLEGE_MANAGEMENT_DBContext context, IHubContext<DashboardHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        //GET: Students
        public IActionResult Index()
        {
            return View();
        }

        //GET all Students
        public async Task<JsonResult> GetAllStudents()
        {
            List<Student> students = await _context.Students
                    .Include(s => s.IdCourseNavigation)
                    .ToListAsync();

            return Json(students);
        }

        //GET students by subject
        public async Task<JsonResult> GetStudentsBySubject(string idSubject)
        {
            int IdSubject = int.Parse(idSubject);
            var studentsInSubjects = await _context.StudentGrades
                    .Include(s => s.IdSubjectNavigation)
                    .Include(s => s.IdStudentRegistrationNumberNavigation)
                    .Where(s => s.IdSubject == IdSubject)
                    .ToListAsync();

            return Json(studentsInSubjects);
        }

        //GET students by course
        public async Task<JsonResult> GetStudentsByCourse(string idCourse, string idSubject)
        {
            int IdCourse = int.Parse(idCourse);
            var studentsInCourses = await _context.Students
                    .Where(s => s.IdCourse == IdCourse)
                    .ToListAsync();

            int IdSubject = int.Parse(idSubject);
            var studentsInSubjects = await _context.StudentGrades
                  .Where(s => s.IdSubject == IdSubject)
                  .ToListAsync();

            var studentsInCourseNotInSubject = studentsInCourses.Where(a => !studentsInSubjects.Select(b => b.IdStudentRegistrationNumber).Contains(a.IdStudentRegistrationNumber));

            return Json(studentsInCourseNotInSubject);
        }

        //GET students by id
        public async Task<JsonResult> GetStudentById(string id)
        {
            int studentId = int.Parse(id);

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.IdStudentRegistrationNumber == studentId);

            return Json(student);
        }

        //POST Insert Student
        public async Task<string> InsertStudentAsync([FromBody] Student student)
        {

            if (student != null)
            {
                var nameExist = await _context.Students.Where(x => x.Name == student.Name).ToListAsync();
                //Verify if name already exists
                if (nameExist.Count() == 0)
                {
                    if (student.Name != "")
                    {
                        _context.Add(student);
                        await _context.SaveChangesAsync();

                        //Atualiza Dashboard
                        var StudentCount = _context.Students.Count();
                        var UpdateDashboard = _hub.Clients.All.SendAsync("getStudentsInfo", new
                        {
                            studentsQtd = StudentCount
                        });

                        return "Sucess! Student Added Successfully";
                    }
                    else
                    {
                        return "Error! Student name cannot be empty";
                    }
                }
                else
                {
                    return "Error! Student already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Student Not Inserted! Try Again";
            }
        }

        //POST Delete Student
        public async Task<string> DeleteStudentAsync([FromBody] Student student)
        {
            if (student != null)
            {

                _context.Students.Attach(student);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                //Atualiza Dashboard
                var StudentCount = _context.Students.Count();
                var UpdateDashboard = _hub.Clients.All.SendAsync("getStudentsInfo", new
                {
                    studentsQtd = StudentCount
                });
                return "Sucess! Student Removed Successfully";
            }
            else
            {
                return "Error! Student Not Deleted! Try Again";
            }
        }

        //POST Update Student
        public async Task<string> UpdateStudentAsync([FromBody] Student student)
        {
            if (student != null)
            {
                //Verify if name already exists
                var nameExist = await _context.Students.Where(x => x.Name == student.Name && x.IdStudentRegistrationNumber != student.IdStudentRegistrationNumber).ToListAsync();

                if (nameExist.Count() == 0)
                {
                    if (student.Name != "")
                    {
                        Student StudentObj = await _context.Students
                    .Where(x => x.IdStudentRegistrationNumber == student.IdStudentRegistrationNumber).
                    FirstOrDefaultAsync();

                        StudentObj.Name = student.Name;
                        StudentObj.IdStudentRegistrationNumber = student.IdStudentRegistrationNumber;
                        StudentObj.IdCourse = student.IdCourse;
                        await _context.SaveChangesAsync();

                        //Atualiza Dashboard
                        var StudentCount = _context.Students.Count();
                        var UpdateDashboard = _hub.Clients.All.SendAsync("getStudentsInfo", new
                        {
                            studentsQtd = StudentCount
                        });

                        return "Sucess! Student Updated Successfully";
                    }
                    else
                    {
                        return "Error! Student name cannot be empty";

                    }
                }
                else
                {
                    return "Error! Student already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Student Not Updated! Try Again";
            }
        }

        //POST Insert Student on Subject
        public async Task<string> InsertStudentOnSubjectAsync([FromBody] StudentGrade student)
        {
            if (student != null)
            {
                //Verify if the student is already in this subject
                var studentToBeAdd = _context.StudentGrades
                   .FirstOrDefault(x => x.IdSubject == student.IdSubject && x.IdStudentRegistrationNumber == student.IdStudentRegistrationNumber);

                if (studentToBeAdd == null)
                {
                    //Verify if the grade is bellow 10
                    if(student.Grade <= 10)
                    {
                        _context.Add(student);
                        await _context.SaveChangesAsync();
                        return "Sucess! Student and Grade Added Successfully to the subject";
                    }
                    else
                    {
                        return "Error! Grade must be btween 0 and 10";
                    }
                }
                else
                {
                    return "Error! Student already exists in this subject";
                }

            }
            else
            {
                return "Error! Student not inserted";
            }
        }

        //POST Remove Student From Subject
        public async Task<string> RemoveStudentFromSubjectAsync([FromBody] StudentGrade student)
        {
            if (student != null)
            {
                var studentToBeRemoved = _context.StudentGrades
                    .FirstOrDefault(x => x.IdSubject == student.IdSubject && x.IdStudentRegistrationNumber == student.IdStudentRegistrationNumber);

                _context.StudentGrades.Attach(studentToBeRemoved);
                _context.StudentGrades.Remove(studentToBeRemoved);
                await _context.SaveChangesAsync();

                return "Sucess! Student Removed Successfully";
            }
            else
            {
                return "Error! Student Not Removed! Try Again";
            }
        }
    }
}
