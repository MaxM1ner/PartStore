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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FormImageManager _formImageUploader;
        private readonly ProductManager _productManager;
        private readonly FeatureManager _featureManager;
        private readonly ProductTypeManager _productTypeManager;
        public ProductsController
            (IWebHostEnvironment hostingEnvironment, 
            FormImageManager formImageUploader, 
            ProductManager productManager, 
            FeatureManager featureManager, 
            ProductTypeManager productTypeManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _formImageUploader = formImageUploader;
            _productManager = productManager;
            _featureManager = featureManager;
            _productTypeManager = productTypeManager;
        }

        // GET: Admin/GetFeatures/{id}
        [Route("Admin/Products/GetFeatures/{id}")]
        [Route("Admin/Products/Edit/GetFeatures/{id}")]
        public async Task<IActionResult> GetFeatures([FromRoute] int id)
        {
            
            var productFeatures = (await _featureManager.GetFeaturesAsync()).Where(x => x.ProductTypeId == id);
            return Json(productFeatures);
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var products = new List<ProductViewModel>();
            foreach (var dbProduct in await _productManager.GetProductsAsync())
            {
                products.Add(dbProduct.ToProductViewModel());
            }
            return View(products);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productManager.GetProductAsync((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product.ToProductViewModel());
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value));

            var defaultType = (await _productTypeManager.GetProductTypesAsync()).FirstOrDefault();

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
                    dbProduct.Features.Add((await _featureManager.GetFeaturesAsync()).Where(x => x.Id == feature).First());
                }
                await _productManager.CreateAsync(dbProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), dbProduct.ProductTypeId);
            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == dbProduct.ProductTypeId).FirstOrDefault();

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
            if (!await _productManager.IsExistAsync((int)id)) return NotFound();
            var product = await _productManager.GetProductAsync((int)id);

            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), product.ProductTypeId);

            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == product.ProductTypeId).FirstOrDefault();

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
            var dbProduct = await _productManager.GetProductAsync(product.Id);
            dbProduct.UpdateProduct(product);
            if (ModelState.IsValid)
            {
                dbProduct.Features.Clear();
                foreach (var feature in product.SelectedFeaturesIds)
                {
                    dbProduct.Features.Add((await _featureManager.GetFeaturesAsync()).Where(x => x.Id == feature).First());
                }
                await _productManager.UpdateAsync(dbProduct);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), dbProduct.ProductTypeId);

            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == dbProduct.ProductTypeId).FirstOrDefault();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");

            ViewData["SelectedFeatures"] = new SelectList((await _featureManager.GetFeaturesAsync()).Where(x => x.ProductTypeId == dbProduct.ProductTypeId).Select(x => new
            {
                id = x.Id,
                name = x.Name + ": " + x.Value
            }), "id", "name");

            return View(dbProduct.ToProductViewModel());
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _productManager.IsExistAsync((int)id)) return NotFound();
            var product = await _productManager.GetProductAsync((int)id);

            return View(product.ToProductViewModel());
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
