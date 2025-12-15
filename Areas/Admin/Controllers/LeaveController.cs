using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaffManagement.Models;
using StaffManagement.Repository.Base;
using StaffManagement.ViewModels;
using System.Linq;

namespace StaffManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var leaves = _unitOfWork.LeaveRequests.FindAll();
            return View(leaves);
        }
        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveRequest model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", model.EmployeeId);
                return View(model);
            }

            _unitOfWork.LeaveRequests.AddOne(model);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
        }

    
        public IActionResult Edit(int id)
        {
            var leave = _unitOfWork.LeaveRequests.FindID(id);

            if (leave == null)
                return NotFound();
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View(leave);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LeaveRequest model)
        {
            if (!ModelState.IsValid)
            {
               ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", model.EmployeeId);
                return View(model);
            }
            _unitOfWork.LeaveRequests.UpdateOne(model);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var leave = _unitOfWork.LeaveRequests.FindID(id);

            if (leave == null)
                return NotFound();

            return View(leave);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var leave = _unitOfWork.LeaveRequests.FindID(id);

            if (leave == null)
                return NotFound();

            _unitOfWork.LeaveRequests.DeleteOne(leave);
            _unitOfWork.CommitChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var leave = _unitOfWork.LeaveRequests.FindID(id);

            if (leave == null)
                return NotFound();

            return View(leave);
        }
    }
}
