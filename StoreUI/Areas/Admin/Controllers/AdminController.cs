using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
   [Authorize(Roles = "Admin")]
    public sealed class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
