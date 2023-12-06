using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using StoreUI.Areas.Admin.ViewModels;
using System.Linq;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class OrdersController : Controller
    {
        private readonly IOrdersService _ordersMangaer;

        public OrdersController(IOrdersService ordersMangaer)
        {
            _ordersMangaer = ordersMangaer;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var types = await _ordersMangaer.GetAllOrdersAsync();
            return types.Count > 0 ?
                        View(types) :
                        Problem("Entity set 'ApplicationDbContext.ProductTypes'  is null.");
        }

        //// GET: Admin/ProductTypes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{

        //    var productType = await _ordersMangaer.GetOrderAsync(id.Value);
        //    if (productType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productType);
        //}

        //// GET: Admin/Orders/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        ////POST: Admin/Orders/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("")] OrderViewModel order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var orderRequest= order.ToOrderAddRequest();
        //        await _ordersMangaer.AddOrderAsync(orderRequest);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productType);
        //}

        //// GET: Admin/Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var order = await _ordersMangaer.GetOrderAsync(id.Value);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order.ToOrderViewModel());
        //}

        //// POST: Admin/Orders/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Value,IsVisible,TypeImagePath")] OrderViewModel productType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dbProductType = productType.ToOrderUpdateRequest();
        //        await _ordersMangaer.UpdateOrderAsync(dbProductType);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productType);
        //}

        //// GET: Admin/Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var order = await _ordersMangaer.GetOrderAsync(id.Value);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order.ToOrderViewModel());
        //}

        //// POST: Admin/Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var productType = await _ordersMangaer.GetOrderAsync(id);
        //    if (productType != null)
        //    {
        //        await _ordersMangaer.RemoveOrderAsync(productType);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
