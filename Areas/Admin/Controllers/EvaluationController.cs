using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaffManagement.Models;
using StaffManagement.Repository.Base;

namespace StaffManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EvaluationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EvaluationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var evaluations = _unitOfWork.Evaluations.FindAll();
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View(evaluations);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Evaluation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", model.EmployeeId);
                return View(model);
            }

            _unitOfWork.Evaluations.AddOne(model);
            _unitOfWork.CommitChanges();

            return RedirectToAction(nameof(Index));
        }

   
        public IActionResult Edit(int id)
        {
            var evaluation = _unitOfWork.Evaluations.FindID(id);
            if (evaluation == null)
                return NotFound();
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View(evaluation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Evaluation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", model.EmployeeId);
                return View(model);
            }

            _unitOfWork.Evaluations.UpdateOne(model);
            _unitOfWork.CommitChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var evaluation = _unitOfWork.Evaluations.FindID(id);
            if (evaluation == null)
                return NotFound();

            return View(evaluation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var evaluation = _unitOfWork.Evaluations.FindID(id);
            if (evaluation == null)
                return NotFound();

            _unitOfWork.Evaluations.DeleteOne(evaluation);
            _unitOfWork.CommitChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var evaluation = _unitOfWork.Evaluations.FindID(id);
            if (evaluation == null) return NotFound();
            return View(evaluation);
        }
    }
}
