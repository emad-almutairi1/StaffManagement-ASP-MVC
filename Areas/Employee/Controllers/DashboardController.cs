using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Repository.Base;
using StaffManagement.ViewModels;
using System.Net;

namespace StaffManagement.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            var employee = _unitOfWork.Employees.GetEmployeeWithDetails(user.Id);
            if (employee == null)
                return NotFound("Employee not found for this user.");

            var model = new EmployeeDashboardVM
            {
                FullName = employee.FullName,
                Email = employee.Email,
                Department = employee.Department,
                HireDate = employee.HireDate,
                TaskCount = employee.TaskItems?.Count ?? 0,
                ProjectsCount = employee.Projects?.Count ?? 0,
                EvaluationsCount = employee.Evaluations?.Count ?? 0,
                LeaveRequestsCount = employee.LeaveRequests?.Count ?? 0,

                LastTasks = employee.TaskItems?
            .OrderByDescending(t => t.DueDate)
            .Take(5)
            .ToList()
            };
     
            return View(model);

        }
        public IActionResult PreviewAttachment(int id)
        {
            var task = _unitOfWork.TaskItems.FindID(id);
            if (task == null || task.AttachmentData == null)
                return NotFound();

            var contentType = task.AttachmentType ?? "application/octet-stream";

            if (contentType.StartsWith("image") || contentType == "application/pdf")
            {
                Response.Headers.Add("Content-Disposition", $"inline; filename={Uri.EscapeDataString(task.AttachmentName ?? "file")}");
                return File(task.AttachmentData, contentType);
            }

            return File(task.AttachmentData, contentType, task.AttachmentName);
        }
    }
}
