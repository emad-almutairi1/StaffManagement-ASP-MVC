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
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {

           var projects = _unitOfWork.Projects.FindAll();
            return View(projects);
        }

        public IActionResult Details(int id)
        {
            var project = _unitOfWork.Projects.FindID(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Project project)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", project.EmployeeId);
                

                return View(project);

            }
            if (project.ClientFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await project.ClientFile.CopyToAsync(ms);
                    project.AttachmentData = ms.ToArray();
                    project.AttachmentType = project.ClientFile.ContentType;
                    project.AttachmentName = project.ClientFile.FileName;
                }
            }
            _unitOfWork.Projects.AddOne(project);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
            // return View(project);
        }

        public IActionResult Edit(int id)
        {
          
            var project = _unitOfWork.Projects.FindID(id);
            if (project == null)
                return NotFound();
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", project.EmployeeId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Project project)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", project.EmployeeId);
                if (project.ClientFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await project.ClientFile.CopyToAsync(ms);
                        project.AttachmentData = ms.ToArray();
                        project.AttachmentType = project.ClientFile.ContentType;
                        project.AttachmentName = project.ClientFile.FileName;
                    }
                }
                _unitOfWork.Projects.UpdateOne(project);
                _unitOfWork.CommitChanges();
                return RedirectToAction(nameof(Index));
               
            }
            return View(project);
        }

        public IActionResult Delete(int id)
        {
            var project = _unitOfWork.Projects.FindID(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _unitOfWork.Projects.FindID(id);
            if (project == null)
                return NotFound();

            _unitOfWork.Projects.DeleteOne(project);
            _unitOfWork.CommitChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PreviewAttachment(int id)
        {
            var project = _unitOfWork.Projects.FindID(id);

            if (project == null || project.AttachmentData == null)
                return NotFound();

            var contentType = project.AttachmentType ?? "application/octet-stream";

            if (contentType.StartsWith("image") || contentType == "application/pdf")
            {
                Response.Headers.Add("Content-Disposition",
                    $"inline; filename={Uri.EscapeDataString(project.AttachmentName ?? "file")}");

                return File(project.AttachmentData, contentType);
            }

            return File(project.AttachmentData, contentType, project.AttachmentName);
        }
    }
}