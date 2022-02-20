using CollegeManagement.Data;
using CollegeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

#nullable disable warnings

namespace CollegeManagement.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly COLLEGE_MANAGEMENT_DBContext _context;

        public SubjectsController(COLLEGE_MANAGEMENT_DBContext context)
        {
            _context = context;
        }

        //GET: Subjects
        public IActionResult Index()
        {
            return View();
        }

        //GET all subjects
        public async Task<JsonResult> GetAllSubjects(string idCourse)
        {
            int IdCourse = int.Parse(idCourse);
            List<Subject> subjects = await _context.Subjects
                .Where(x => x.IdCourse == IdCourse)
                .Include(x => x.IdTeacherNavigation)
                .ToListAsync();

            //Count Numbers of Students In a Subject
            var studentsCountGrades = await _context.StudentGrades
                    .GroupBy(l => l.IdSubject)
                    .Select(sg => new
                    {
                        sg.Key,
                        StudentsQty = sg.Select(l => l.IdStudentRegistrationNumber).Distinct().Count(),
                        AvgGrade = sg.Average(l => l.Grade)
                    })
                .ToListAsync();

            var subjectsInfoFinal = (
                    from s in subjects
                    join sg in studentsCountGrades on s.IdSubject equals sg.Key into ssg
                    from StudentInSubject in ssg.DefaultIfEmpty()
                    select new
                    {
                        s.IdSubject,
                        s.DsSubject,
                        s.IdTeacherNavigation.IdTeacher,
                        s.IdTeacherNavigation.Name,
                        s.IdTeacherNavigation.Birthday,
                        s.IdTeacherNavigation.Salary,
                        StudentsQty  = (StudentInSubject != null) ? StudentInSubject.StudentsQty : default(int),
                        AvgGrade = (StudentInSubject != null) ? StudentInSubject.AvgGrade : default(decimal)
                    });

            return Json(subjectsInfoFinal);
        }

        //GET subjects by id
        public async Task<JsonResult> GetSubjectById(string id)
        {
            int subjectId = int.Parse(id);

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.IdSubject == subjectId);

            return Json(subject);
        }

        //POST Insert Subject
        public async Task<string> InsertSubjectAsync([FromBody] Subject subject)
        {

            if (subject != null)
            {
                var nameExist = await _context.Subjects.Where(x => x.DsSubject == subject.DsSubject && x.IdCourse == subject.IdCourse).ToListAsync();
                //Verify if name already exists
                if (nameExist.Count() == 0)
                {
                    if (subject.DsSubject != "")
                    {
                        _context.Add(subject);
                        await _context.SaveChangesAsync();
                        return "Sucess! Subject Added Successfully";
                    }
                    else
                    {
                        return "Error! Subject name cannot be empty";
                    }
                }
                else
                {
                    return "Error! Subject already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Subject Not Inserted! Try Again";
            }
        }

        //POST Delete Subject
        public async Task<string> DeleteSubjectAsync([FromBody] Subject subject)
        {
            if (subject != null)
            {

                _context.Subjects.Attach(subject);
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();

                return "Sucess! Subject Removed Successfully";
            }
            else
            {
                return "Error! Subject Not Deleted! Try Again";
            }
        }

        //POST Update Subject
        public async Task<string> UpdateSubjectAsync([FromBody] Subject subject)
        {
            if (subject != null)
            {
                //Verify if name already exists
                var nameExist = await _context.Subjects.Where(x => x.DsSubject == subject.DsSubject && x.IdSubject != subject.IdSubject && x.IdCourse == subject.IdCourse).ToListAsync();

                if (nameExist.Count() == 0)
                {
                    if (subject.DsSubject != "")
                    {
                        Subject SubjectObj = await _context.Subjects
                    .Where(x => x.IdSubject == subject.IdSubject).
                    FirstOrDefaultAsync();

                        SubjectObj.DsSubject = subject.DsSubject;
                        SubjectObj.IdTeacher = subject.IdTeacher;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return "Error! Subject name cannot be empty";

                    }
                }
                else
                {
                    return "Error! Subject already exists. Try a diferent name";
                }

                return "Sucess! Subject Updated Successfully";
            }
            else
            {
                return "Error! Subject Not Updated! Try Again";
            }
        }
    }
}




