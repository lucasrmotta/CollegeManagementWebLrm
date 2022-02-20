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

namespace CollegeManagement.Controllers
{
    public class TeachersController : Controller
    {
        private readonly COLLEGE_MANAGEMENT_DBContext _context;

        public TeachersController(COLLEGE_MANAGEMENT_DBContext context)
        {
            _context = context;
        }

        //GET: Teachers
        public IActionResult Index()
        {
            return View();
        }

        //GET all Teachers
        public async Task<JsonResult> GetAllTeachers()
        {
            List<Teacher> teachers = await _context.Teachers
                    .ToListAsync();

            return Json(teachers);
        }
        //GET teachers by id
        public async Task<JsonResult> GetTeacherById(string id)
        {
            int teacherId = int.Parse(id);

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.IdTeacher == teacherId);

            return Json(teacher);
        }

        //POST Insert Teacher
        public async Task<string> InsertTeacherAsync([FromBody] Teacher teacher)
        {

            if (teacher != null)
            {
                var nameExist = await _context.Teachers.Where(x => x.Name == teacher.Name).ToListAsync();
                //Verify if name already exists
                if (nameExist.Count() == 0)
                {
                    if (teacher.Name != "")
                    {
                        _context.Add(teacher);
                        await _context.SaveChangesAsync();
                        return "Sucess! Teacher Added Successfully";
                    }
                    else
                    {
                        return "Error! Teacher name cannot be empty";
                    }
                }
                else
                {
                    return "Error! Teacher already exists. Try a diferent name";
                }
            }
            else
            {
                return "Error! Teacher Not Inserted! Try Again";
            }
        }

        //POST Delete Teacher
        public async Task<string> DeleteTeacherAsync([FromBody] Teacher teacher)
        {
            if (teacher != null)
            {

                _context.Teachers.Attach(teacher);
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();

                return "Sucess! Teacher Removed Successfully";
            }
            else
            {
                return "Error! Teacher Not Deleted! Try Again";
            }
        }

        //POST Update Teacher
        public async Task<string> UpdateTeacherAsync([FromBody] Teacher teacher)
        {
            if (teacher != null)
            {
                //Verify if name already exists
                var nameExist = await _context.Teachers.Where(x => x.Name == teacher.Name && x.IdTeacher != teacher.IdTeacher).ToListAsync();

                if (nameExist.Count() == 0)
                {
                    if (teacher.Name != "")
                    {
                        Teacher TeacherObj = await _context.Teachers
                    .Where(x => x.IdTeacher == teacher.IdTeacher).
                    FirstOrDefaultAsync();

                        TeacherObj.Name = teacher.Name;
                        TeacherObj.IdTeacher = teacher.IdTeacher;
                        TeacherObj.Birthday = teacher.Birthday;
                        TeacherObj.Salary = teacher.Salary;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return "Error! Teacher name cannot be empty";

                    }
                }
                else
                {
                    return "Error! Teacher already exists. Try a diferent name";
                }

                return "Sucess! Teacher Updated Successfully";
            }
            else
            {
                return "Error! Teacher Not Updated! Try Again";
            }
        }
    }
}
