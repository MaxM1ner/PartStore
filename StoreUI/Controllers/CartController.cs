using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Cart;
using Services;
using StoreUI.Areas.Admin.ViewModels;
using StoreUI.ViewModels;
using System.Security.Claims;

namespace StoreUI.Controllers
{
    [Authorize]
    public sealed class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) 
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetAllProductsAsync(userId);
            return View(result);
        }
        public async Task<IActionResult> RemoveProduct(int cartProductId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = new CartProductResponse() { CustomerId = Guid.Parse(userId), CartProductId = cartProductId};
            await _cartService.RemoveProductAsync(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddProduct(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var request = new CartAddRequest(Guid.Parse(userId), id);
            await _cartService.AddProductAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
