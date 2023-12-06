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
using ServiceContracts;

namespace StoreUI.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly IProductTypeService _productTypeManager;

        public HomeController(IProductTypeService productTypeManager)
        {
            _productTypeManager = productTypeManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productTypeManager.GetProductTypesAsync());
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