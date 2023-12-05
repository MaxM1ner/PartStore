using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Comment;
using ServiceContracts.DTO.Order;
using Services;
using System.Security.Claims;

namespace StoreUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCommentService _productCommentManager;
        private readonly IProductService _productManager;
        public ProductController(IProductService productManager, IProductCommentService productCommentManager)
        {
            _productManager = productManager;
            _productCommentManager = productCommentManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var product = await _productManager.GetProductAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentValue, int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newAddRequest = new CommentAddRequest(userId, productId, commentValue);
            await _productCommentManager.AddCommentAsync(newAddRequest);
            return RedirectToAction("Details", new { id = productId });
        }
    }
}
