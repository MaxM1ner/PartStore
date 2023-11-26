using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using DataAccess;
using Entities.Models;
using System.Diagnostics;
using StoreUI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts.DTO.Order;
using System.Security.Claims;
using Services;

namespace StoreUI.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ProductCommentManager _productCommentManager;
        public HomeController(ILogger<HomeController> logger,  ApplicationDbContext context, ProductCommentManager productCommentManager)
        {
            _logger = logger;
            _context = context;
            _productCommentManager = productCommentManager;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _context.ProductTypes.Where(x => x.Visible).ToListAsync();
            return View(types);
        }
        public async Task<IActionResult> Catalog(int? typeid)
        {
            var catalogProducts = new List<Product>();
            var types = await _context.ProductTypes.Include(x => x.Products).ThenInclude(y => y.Images).ToListAsync();
            if(typeid != null)
            foreach (var type in await _context.ProductTypes.Include(x => x.Products).Where(x => x.Visible).ToListAsync())
            {
                    if(type.Id == typeid)
                    catalogProducts.AddRange(type.Products.Where(x => x.Quantity > 0 && x.IsVisible).Take(15));
            }
            return View(new CatalogViewModel() { Products = catalogProducts, ProductTypes = types});
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.Products.
                Where(x => x.Id == id && x.IsVisible).
                Include(x => x.Images).
                Include(x => x.Features).
                Include(x => x.Type).
                Include(x => x.Comments).
                ThenInclude(x => x.Customer).FirstAsync());
        }

        public async Task<IActionResult> AddComment(string commentValue, int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newAddRequest = new CommentAddRequest(userId, productId, commentValue);
            await _productCommentManager.AddCommentAsync(newAddRequest);
            return RedirectToAction("Details", new { id = productId});
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