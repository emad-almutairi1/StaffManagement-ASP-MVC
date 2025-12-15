using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Models;
using StaffManagement.Repository.Base;
using StaffManagement.ViewModels;
using System.Linq;
using System.Security.Claims;


namespace StaffManagement.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class ProfileController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public ProfileController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            
            var employee = _unitOfWork.Employees.GetEmployeeWithDetails(user.Id);
            if (employee == null)
            {
                return View("NoEmployee");
            }

            return View(employee);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.Employees.FindID(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StaffManagement.Models.Employee employee)
        {
            if (employee == null) return NotFound();

            if (ModelState.IsValid)
            {

                return View(employee);
            }
            _unitOfWork.Employees.UpdateOne(employee);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
        }
    }
    }

