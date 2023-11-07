using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using NuGet.Packaging;
using Services;
using DataAccess;
using DataAccess.Migrations;
using Entities.Models;
using StoreUI.Areas.Admin.ViewModels;
using StoreUI.Extensions;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context; //Remove

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FormImageManager _formImageUploader;
        private readonly ProductManager _productManager;
        public ProductsController(IWebHostEnvironment hostingEnvironment, FormImageManager formImageUploader, ProductManager productManager, ApplicationDbContext context)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _formImageUploader = formImageUploader;
            _productManager = productManager;
        }

        // GET: Admin/GetFeatures/{id}
        [Route("Admin/Products/GetFeatures/{id}")]
        [Route("Admin/Products/Edit/GetFeatures/{id}")]
        public async Task<IActionResult> GetFeatures([FromRoute] int id)
        {

            var productFeatures = _context.Features.Where(x => x.ProductTypeId == id);
            return Json(await productFeatures.ToListAsync());
        }

        // GET: Admin/Products
        public IActionResult Index()
        {
            var products = new List<ProductViewModel>();
            foreach (var dbProduct in _productManager.GetProducts())
            {
                products.Add(dbProduct.ToProductViewModel());
            }
            return View(products);
        }

        // GET: Admin/Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetProduct((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product.ToProductViewModel());
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value");

            var defaultType = await _context.ProductTypes.Include(f => f.Features).FirstOrDefaultAsync();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId,SelectedFeaturesIds,FormImages")] ProductViewModel product)
        {
            var dbProduct = product.ToProduct();
            if (ModelState.IsValid)
            {
                if (product.FormImages != null && product.FormImages.Count > 0)
                {
                    foreach (var imageFile in product.FormImages)
                    {
                        if (imageFile.Length > 0)
                        {
                            string fileName = _formImageUploader.UploadImage(imageFile).Result;
                            dbProduct.Images.Add(new ProductImage()
                            {
                                Path = fileName,
                                Product = dbProduct,
                                ProductId = product.Id
                            });
                        }
                    }
                }
                foreach (var feature in product.SelectedFeaturesIds)
                {
                    dbProduct.Features.Add(await _context.Features.Where(x => x.Id == feature).FirstAsync());
                }
                await _productManager.Create(dbProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", dbProduct.ProductTypeId);
            var defaultType = await _context.ProductTypes.Where(x => x.Id == dbProduct.ProductTypeId).FirstOrDefaultAsync();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");
            return View(dbProduct);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_productManager.IsExist((int)id)) return NotFound();
            var product = _productManager.GetProduct((int)id);

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", product.ProductTypeId);

            var defaultType = await _context.ProductTypes.Where(x => x.Id == product.ProductTypeId).Include(p => p.Features).FirstOrDefaultAsync();

            var selectListItems = product.Features.Where(x => x.ProductTypeId == product.ProductTypeId);

            ViewData["SelectedFeatures"] = new SelectList(selectListItems.Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name", product.Features.Select(x => x.Id).ToArray());

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Except(selectListItems).Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");


            return View(product.ToProductViewModel());
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId,SelectedFeaturesIds")] ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var dbProduct = _productManager.GetProduct(product.Id);
            dbProduct.UpdateProduct(product);
            if (ModelState.IsValid)
            {
                dbProduct.Features.Clear();
                foreach (var feature in product.SelectedFeaturesIds)
                {
                    dbProduct.Features.Add(await _context.Features.Where(x => x.Id == feature).FirstAsync());
                }
                await _productManager.Edit(dbProduct);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", dbProduct.ProductTypeId);

            var defaultType = await _context.ProductTypes.Where(x => x.Id == dbProduct.ProductTypeId).FirstOrDefaultAsync();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");

            ViewData["SelectedFeatures"] = new SelectList(_context.Features.Where(x => x.ProductTypeId == dbProduct.ProductTypeId).Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");

            return View(dbProduct.ToProductViewModel());
        }

        // GET: Admin/Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_productManager.IsExist((int)id)) return NotFound();
            var product = _productManager.GetProduct((int)id);

            return View(product.ToProductViewModel());
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productManager.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
