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

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class ProductCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductComments.Include(p => p.Customer).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ProductComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductComments == null)
            {
                return NotFound();
            }

            var productComment = await _context.ProductComments
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productComment == null)
            {
                return NotFound();
            }

            return View(productComment);
        }

        // GET: Admin/ProductComments/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/ProductComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,ProductId,CustomerId")] ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productComment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productComment.ProductId);
            return View(productComment);
        }

        // GET: Admin/ProductComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductComments == null)
            {
                return NotFound();
            }

            var productComment = await _context.ProductComments.FindAsync(id);
            if (productComment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productComment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productComment.ProductId);
            return View(productComment);
        }

        // POST: Admin/ProductComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ProductId,CustomerId")] ProductComment productComment)
        {
            if (id != productComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCommentExists(productComment.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productComment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productComment.ProductId);
            return View(productComment);
        }

        // GET: Admin/ProductComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductComments == null)
            {
                return NotFound();
            }

            var productComment = await _context.ProductComments
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productComment == null)
            {
                return NotFound();
            }

            return View(productComment);
        }

        // POST: Admin/ProductComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductComments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductComments'  is null.");
            }
            var productComment = await _context.ProductComments.FindAsync(id);
            if (productComment != null)
            {
                _context.ProductComments.Remove(productComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCommentExists(int id)
        {
          return (_context.ProductComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
