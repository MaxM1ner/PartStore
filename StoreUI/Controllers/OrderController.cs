using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceContracts;
using ServiceContracts.DTO.Order;
using Services;
using System.Security.Claims;

namespace StoreUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersService _orderService;
        public OrderController(IOrdersService orderService)
        {
            _orderService = orderService;

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
