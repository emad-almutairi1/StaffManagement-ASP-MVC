using Microsoft.AspNetCore.Mvc;

namespace StaffManagement.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
