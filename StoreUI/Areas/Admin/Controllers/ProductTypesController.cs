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
using StoreUI.Areas.Admin.ViewModels;
using Services;
using ServiceContracts;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFormImageService _formImageUploader;
        private readonly IProductTypeService _productTypeService;

        public ProductTypesController(ApplicationDbContext context, IFormImageService formImageManager, IProductTypeService productTypeService)
        {
            _context = context;
            _formImageUploader = formImageManager;
            _productTypeService = productTypeService;
        }

        // GET: Admin/ProductTypes
        public async Task<IActionResult> Index()
        {
            var types = await _productTypeService.GetProductTypesAsync();
              return types.Count > 0 ? 
                          View(types.Select(x => x.ToProductTypeViewModel())) :
                          Problem("Entity set 'ApplicationDbContext.ProductTypes'  is null.");
        }

        // GET: Admin/ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !await _productTypeService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetProductTypeAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType.ToProductTypeViewModel());
        }

        // GET: Admin/ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,IsVisible,TypeImage")] ProductTypeViewModel productType)
        {
            if (ModelState.IsValid)
            {
                var dbProductType = productType.ToProductTypeAddRequest();
                dbProductType.TypeImagepath = await _formImageUploader.UploadImage(productType.TypeImage);
                await _productTypeService.CreateAsync(dbProductType);
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: Admin/ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !await _productTypeService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetProductTypeAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType.ToProductTypeViewModel());
        }

        // POST: Admin/ProductTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,IsVisible,TypeImage")] ProductTypeViewModel productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbProductType = productType.ToProductTypeUpdateRequest();
                dbProductType.TypeImagepath = await _formImageUploader.UploadImage(productType.TypeImage);
                await _productTypeService.UpdateAsync(dbProductType);
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: Admin/ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await _productTypeService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetProductTypeAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType.ToProductTypeViewModel());
        }

        // POST: Admin/ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _productTypeService.GetProductTypeAsync(id);
            if (productType != null)
            {
                await _productTypeService.DeleteAsync(productType);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
