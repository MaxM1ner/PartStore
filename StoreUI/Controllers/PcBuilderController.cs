using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreUI.Controllers
{
    public class PcBuilderController : Controller
    {
        // GET: PcBuilderController
        [Route("PcBuilder")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PcBuilderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PcBuilderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PcBuilderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PcBuilderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PcBuilderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PcBuilderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PcBuilderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
