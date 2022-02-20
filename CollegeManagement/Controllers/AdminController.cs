using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Controllers
{
    public class AdminController : Controller
    {
        //GET Courses
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View();
        }
    }
}
