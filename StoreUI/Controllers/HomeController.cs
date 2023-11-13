﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using DataAccess;
using Entities.Models;
using System.Diagnostics;
using StoreUI.ViewModels;

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

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Catalog()
        {
            var catalogProducts = new List<Product>();
            var types = await _context.ProductTypes.Include(x => x.Products).ThenInclude(y => y.Images).ToListAsync();
            foreach (var type in await _context.ProductTypes.Include(x => x.Products).ToListAsync())
            {
                catalogProducts.AddRange(type.Products.Where(x => x.Quantity > 0 && x.IsVisible).Take(5));
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