using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using DataAccess;
using Entities.Models;
using System.Diagnostics;
using StoreUI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreUI.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,  ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _context.ProductTypes.ToListAsync();
            return View(types);
        }
        public async Task<IActionResult> Catalog(int? typeid)
        {
            var catalogProducts = new List<Product>();
            var types = await _context.ProductTypes.Include(x => x.Products).ThenInclude(y => y.Images).ToListAsync();
            if(typeid != null)
            foreach (var type in await _context.ProductTypes.Include(x => x.Products).ToListAsync())
            {
                    if(type.Id == typeid)
                    catalogProducts.AddRange(type.Products.Where(x => x.Quantity > 0 && x.IsVisible).Take(15));
            }
            return View(new CatalogViewModel() { Products = catalogProducts, ProductTypes = types});
        }
        public IActionResult Details(int id)
        {
            return View( _context.Products.
                Where(x => x.Id == id && x.IsVisible).
                Include(x => x.Images).
                Include(x => x.Features).
                Include(x => x.Type).
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