using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Data;
using StaffManagement.Models;
using StaffManagement.ViewModels;

namespace StaffManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = clsRoles.roleAdmin)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            var model = new DashboardViewModel
            {
                Employees = _db.Employees.ToList(),
                EmployeesCount = _db.Employees.Count(),
                ProjectsCount = _db.Projects.Count(),
                TasksCount = _db.TaskItems.Count(),
                LeaveRequestsCount = _db.LeaveRequests.Count(),
                EvaluationsCount = _db.Evaluations.Count()
            };

            return View(model);
        }
       
    }
}
