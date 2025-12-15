using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Models;
using StaffManagement.Repository.Base;

namespace StaffManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeesController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var employee = _unitOfWork.Employees.FindAll();
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public IActionResult Create()
        {
            return View(new StaffManagement.Models.Employee
            {
                HireDate = DateTime.Today
            });
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffManagement.Models.Employee employee, string password)
         {
         if (ModelState.IsValid)
          {
                return View(employee);
            }
            var user = new IdentityUser
            {
                UserName = employee.Email,
                Email = employee.Email
            };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(employee);
            }

          
            if (employee.ClientFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await employee.ClientFile.CopyToAsync(ms);
                    employee.ImageData = ms.ToArray();
                    employee.ImageType = employee.ClientFile.ContentType;
                }
            }
            employee.UserId = user.Id;

            _unitOfWork.Employees.AddOne(employee);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
            
          }

        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.Employees.FindID(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditAsync(StaffManagement.Models.Employee employee)
         {
           if (ModelState.IsValid)
          {

                return View(employee);
            }

            if (employee.ClientFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await employee.ClientFile.CopyToAsync(ms);
                    employee.ImageData = ms.ToArray();
                    employee.ImageType = employee.ClientFile.ContentType;
                }
            }
            _unitOfWork.Employees.UpdateOne(employee);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
          }

        public IActionResult Delete(int id)
        {
            var emp = _unitOfWork.Employees.FindID(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var emp = _unitOfWork.Employees.FindID(id);
            if (emp != null)
            {
                _unitOfWork.Employees.DeleteOne(emp);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string userId)
        {
            var emp = _unitOfWork.Employees.GetEmployeeWithDetails(userId);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
    }
}
