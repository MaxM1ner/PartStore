using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StoreUI.Data;
using StoreUI.Models;
using System.Diagnostics;

namespace StoreUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,  ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Catalog()
        {
            return View(_context.
                Products.
                Where(x => x.IsVisible && x.Quantity > 0).
                Include(x => x.Images).
                ToList());
        }
        public IActionResult Details(int id)
        {
            return View( _context.Products.
                Where(x => x.Id == id && x.IsVisible).
                Include(x => x.Images).
                Include(x => x.Features).
                First());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}