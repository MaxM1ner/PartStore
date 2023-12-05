using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Entities.Models;
using ServiceContracts;
using StoreUI.Areas.Admin.ViewModels;
using ServiceContracts.DTO.Product;
using DataAccess.Migrations;
using ServiceContracts.DTO.ProductType;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class ProductCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IProductCommentService _productCommentService;
        public ProductCommentsController(ApplicationDbContext context, IProductService productService, IProductCommentService productCommentService)
        {
            _context = context;
            _productService = productService;
            _productCommentService = productCommentService;
        }

        // GET: Admin/ProductComments
        public async Task<IActionResult> Index()
        {
            var comments = await _productCommentService.GetAllCommentsAsync((await _productService.GetProductsAsync()).First().Id);
            return View(comments.Select(x => x.ToCommentViewModel()));
        }

        // GET: Admin/ProductComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !await _productCommentService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var comment = await _productCommentService.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, nameof(Customer.Id), nameof(Customer.UserName));
            ViewData["ProductId"] = new SelectList(await _productService.GetProductsAsync(), nameof(ProductResponse.Id), nameof(ProductResponse.Name));
            return View(comment.ToCommentViewModel());
        }

        // GET: Admin/ProductComments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, nameof(Customer.Id), nameof(Customer.UserName));
            ViewData["ProductId"] = new SelectList(await _productService.GetProductsAsync(), nameof(ProductResponse.Id), nameof(ProductResponse.Name));
            return View();
        }

        // POST: Admin/ProductComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Value,ProductId,CustomerId")] CommentViewModel productComment)
        {
            if (ModelState.IsValid)
            {
                var dbcomment = productComment.ToCommentAddRequest();
                await _productCommentService.AddCommentAsync(dbcomment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, nameof(Customer.Id), nameof(Customer.UserName));
            ViewData["ProductId"] = new SelectList(await _productService.GetProductsAsync(), nameof(ProductResponse.Id), nameof(ProductResponse.Name));
            return View(productComment);
        }

        // GET: Admin/ProductComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !await _productCommentService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var comment = await _productCommentService.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, nameof(Customer.Id), nameof(Customer.UserName));
            ViewData["ProductId"] = new SelectList(await _productService.GetProductsAsync(), nameof(ProductResponse.Id), nameof(ProductResponse.Name));
            return View(comment.ToCommentViewModel());
        }

        // POST: Admin/ProductComments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Value,ProductId,CustomerId")] CommentViewModel productComment)
        {
            if (id != productComment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productCommentService.UpdateCommentAsync(productComment.ToCommentUpdateRequest());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productCommentService.IsExistAsync(productComment.CommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, nameof(Customer.Id), nameof(Customer.UserName));
            ViewData["ProductId"] = new SelectList(await _productService.GetProductsAsync(), nameof(ProductResponse.Id), nameof(ProductResponse.Name));
            return View(productComment);
        }

        // GET: Admin/ProductComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await _productCommentService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var comment = await _productCommentService.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment.ToCommentViewModel());
        }

        // POST: Admin/ProductComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _productCommentService.GetCommentAsync(id);
            if (comment != null)
            {
                await _productCommentService.RemoveCommentByIdAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
