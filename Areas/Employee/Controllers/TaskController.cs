using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Repository.Base;

namespace StaffManagement.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class TaskController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
