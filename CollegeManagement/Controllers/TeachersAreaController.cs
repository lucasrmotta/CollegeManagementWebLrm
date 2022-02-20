using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Controllers
{
    public class TeachersAreaController : Controller
    {
        public IActionResult StudentsInSubjects()
        {
            return View();
        }
        public IActionResult SubjectsGrades()
        {
            return View();
        }
    }
}
