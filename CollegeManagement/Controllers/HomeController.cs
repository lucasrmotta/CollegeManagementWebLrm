using CollegeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using CollegeManagement.HubConfig;
using CollegeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly COLLEGE_MANAGEMENT_DBContext _context;
        private readonly IHubContext<DashboardHub> _hub;

        public HomeController(ILogger<HomeController> logger, COLLEGE_MANAGEMENT_DBContext context, IHubContext<DashboardHub> hub)
        {
            _logger = logger;
            _hub = hub;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}