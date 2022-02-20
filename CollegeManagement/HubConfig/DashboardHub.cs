using CollegeManagement.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.HubConfig
{
    public class DashboardHub : Hub
    {
        private readonly COLLEGE_MANAGEMENT_DBContext _context;
        public DashboardHub(COLLEGE_MANAGEMENT_DBContext context)
        {
            _context = context;
        }

        public async Task GetInfo()
        {
            var CoursesCount = await _context.Courses.CountAsync();
            var TeachersCount = await _context.Teachers.CountAsync();
            var StudentsCount = await _context.Students.CountAsync();

            await Clients.All.SendAsync("getCoursesInfo", new
            {
                coursesQtd = CoursesCount
            });

            await Clients.All.SendAsync("getTeachersInfo", new
            {
                teachersQtd = TeachersCount
            });

            await Clients.All.SendAsync("getStudentsInfo", new
            {
                studentsQtd = StudentsCount
            });
        }
    }
}
