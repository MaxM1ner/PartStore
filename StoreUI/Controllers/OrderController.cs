using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceContracts.DTO.Order;
using Services;
using System.Security.Claims;

namespace StoreUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrdersService _orderService;
        private readonly CartService _cartService;
        public OrderController(OrdersService orderService, CartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(string address)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderAddRequest orderAddRequest = new(Guid.Parse(userId), address);
            await _orderService.AddOrderAsync(orderAddRequest);
            return Ok("Order Created");
        }
    }
}
