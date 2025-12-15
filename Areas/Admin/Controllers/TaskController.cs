using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaffManagement.Models;
using StaffManagement.Repository.Base;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var tasks = _unitOfWork.TaskItems.FindAll();
           
            return View(tasks);

        }
        // GET: Admin/Task/Create
        public IActionResult Create()
        {
           ViewBag.Projects = new SelectList((System.Collections.IEnumerable)_unitOfWork.Projects.FindAll(), "Id", "Name");
          ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName");
            return View();
        }

        // POST: Admin/Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(TaskItem tasks)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Projects = new SelectList((System.Collections.IEnumerable)_unitOfWork.Projects.FindAll(), "Id", "Name", tasks.ProjectId);
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", tasks.EmployeeId);
                if (tasks.ClientFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await tasks.ClientFile.CopyToAsync(ms);
                        tasks.AttachmentData = ms.ToArray();
                        tasks.AttachmentType = tasks.ClientFile.ContentType;
                        tasks.AttachmentName = tasks.ClientFile.FileName;
                    }
                }
                _unitOfWork.TaskItems.AddOne(tasks);
                _unitOfWork.CommitChanges();
                return RedirectToAction(nameof(Index));

              
            }

           
            return View(tasks);
        }
        public IActionResult Edit(int id)
        {
            var tasks = _unitOfWork.TaskItems.FindID(id);
            if (tasks == null) return NotFound();

            ViewBag.Projects = new SelectList((System.Collections.IEnumerable)_unitOfWork.Projects.FindAll(), "Id", "Name", tasks.ProjectId);
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", tasks.EmployeeId);
            return View(tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TaskItem tasks)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Projects = new SelectList((System.Collections.IEnumerable)_unitOfWork.Projects.FindAll(), "Id", "Name", tasks.ProjectId);
                ViewBag.Employees = new SelectList((System.Collections.IEnumerable)_unitOfWork.Employees.FindAll(), "Id", "FullName", tasks.EmployeeId);
                if (tasks.ClientFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await tasks.ClientFile.CopyToAsync(ms);
                        tasks.AttachmentData = ms.ToArray();
                        tasks.AttachmentType = tasks.ClientFile.ContentType;
                        tasks.AttachmentName = tasks.ClientFile.FileName;
                    }
                }
                _unitOfWork.TaskItems.UpdateOne(tasks);
                _unitOfWork.CommitChanges();
                return RedirectToAction(nameof(Index));
            }

           
            return View(tasks);
        }
        public IActionResult Delete(int id)
        {
            var tasks = _unitOfWork.TaskItems.FindID(id);
            if (tasks == null) return NotFound();
            return View(tasks);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tasks = _unitOfWork.TaskItems.FindID(id);
            if (tasks == null) return NotFound();

            _unitOfWork.TaskItems.DeleteOne(tasks);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var tasks = _unitOfWork.TaskItems.FindID(id);
            if (tasks == null) return NotFound();
            return View(tasks);
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
