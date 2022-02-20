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
    public class CoursesController : Controller
    {
        private readonly COLLEGE_MANAGEMENT_DBContext _context;
        private readonly IHubContext<DashboardHub> _hub;

        public CoursesController(COLLEGE_MANAGEMENT_DBContext context, IHubContext<DashboardHub> hub)
        {
            _hub = hub;
            _context = context;
        }

        //GET: Courses
        public IActionResult Index()
        {
            return View();
        }

        //GET all Courses
        public async Task<JsonResult> GetAllCourses()
        {
            //List All Courses
            var courses = await _context.Courses
                    .Include(Co => Co.Subjects)
                    .Include(Co => Co.Students)
                    .Select(Co => new
                    {
                        Co.IdCourse,
                        Co.DsCourse,
                        StudentsQty = Co.Students.Count(),
                        SubjectsQty = Co.Subjects.Count()

                    })
                    .ToListAsync();

            //List All Students In Courses
            var students = await _context.Students
                    .ToListAsync();

            //List All Students Grades
            var studentsGrades = await _context.StudentGrades
                    .ToListAsync();

            // Join Course and Students
            var CourseStudents = (
                from c in courses
                join st in students on c.IdCourse equals st.IdCourse  into cst
                from courseStudents in cst.DefaultIfEmpty(new Student())
                join sg in studentsGrades on courseStudents.IdStudentRegistrationNumber equals sg.IdStudentRegistrationNumber into cstg
                from courseStudentsGrades in cstg.DefaultIfEmpty(new StudentGrade())
                select new
                {
                    c.IdCourse,
                    c.DsCourse,
                    c.SubjectsQty,
                    c.StudentsQty,
                    IdStudent = (int?) courseStudentsGrades.IdStudentRegistrationNumber ?? 0,
                    GradeOfStudent = (float?) courseStudentsGrades.Grade ??0
                });

            //Group by and Calculate Average
            var courseInfoFinal = (
                from StudentCourseGrade in CourseStudents
                group StudentCourseGrade by new { StudentCourseGrade.IdCourse, StudentCourseGrade.DsCourse, StudentCourseGrade.StudentsQty, StudentCourseGrade.SubjectsQty } into g
                select new
                {
                    g.Key.IdCourse,
                    g.Key.DsCourse,
                    g.Key.SubjectsQty,
                    g.Key.StudentsQty,
                    AvgGrade = g.Average(courseInfoFinal => courseInfoFinal.GradeOfStudent)
                });

            return Json(courseInfoFinal);
        }

        //GET courses by id
        public async Task<JsonResult> GetCourseById(string id)
        {
            int courseId = int.Parse(id);

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.IdCourse == courseId);

            return Json(course);
        }

        //POST Insert Course
        public async Task<string> InsertCourseAsync([FromBody] Course course)
        {
            if (course != null)
            {
                var nameExist = await _context.Courses.Where(x => x.DsCourse == course.DsCourse).ToListAsync();
                //Verify if name already exists
                if (nameExist.Count() == 0)
                {
                    if (course.DsCourse != "")
                    {
                        _context.Add(course);
                        await _context.SaveChangesAsync();

                        //Atualiza Dashboard
                        var CoursesCount = _context.Courses.Count();
                        var UpdateDashboard =  _hub.Clients.All.SendAsync("getCoursesInfo", new
                        {
                            coursesQtd = CoursesCount
                        });

                        return "Sucess! Course Added Successfully";
                    }
                    else
                    {
                        return "Error! Course name cannot be empty";
                    }
                }
                else
                {
                    return "Error! Course already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Course Not Inserted! Try Again";
            }
        }

        //POST Delete Course
        public async Task<string> DeleteCourseAsync([FromBody] Course course)
        {
            if (course != null)
            {

                _context.Courses.Attach(course);
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                //Atualiza Dashboard
                var CoursesCount = _context.Courses.Count();
                var UpdateDashboard = _hub.Clients.All.SendAsync("getCoursesInfo", new
                {
                    coursesQtd = CoursesCount
                });


                return "Sucess! Course Removed Successfully";
            }
            else
            {
                return "Error! Course Not Deleted! Try Again";
            }
        }

        //POST Update Course
        public async Task<string> UpdateCourseAsync([FromBody] Course course)
        {
            if (course != null)
            {
                //Verify if name already exists
                var nameExist = await _context.Courses.Where(x => x.DsCourse == course.DsCourse && x.IdCourse != course.IdCourse).ToListAsync();

                if (nameExist.Count() == 0)
                {
                    if (course.DsCourse != "")
                    {
                        Course CourseObj = await _context.Courses
                    .Where(x => x.IdCourse == course.IdCourse).
                    FirstOrDefaultAsync();

                        CourseObj.DsCourse = course.DsCourse;
                        CourseObj.IdCourse = course.IdCourse;
                        await _context.SaveChangesAsync();

                        //Atualiza Dashboard
                        var CoursesCount = _context.Courses.Count();
                        var UpdateDashboard = _hub.Clients.All.SendAsync("getCoursesInfo", new
                        {
                            coursesQtd = CoursesCount
                        });

                        return "Sucess! Course Updated Successfully";
                    }
                    else
                    {
                        return "Error! Course name cannot be empty";

                    }
                }
                else
                {
                    return "Error! Course already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Course Not Updated! Try Again";
            }
        }
    }
}
