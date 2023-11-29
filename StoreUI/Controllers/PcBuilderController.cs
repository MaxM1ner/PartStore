using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreUI.Controllers
{
    public sealed class PcBuilderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
