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
using ServiceContracts.DTO.Product;
using ServiceContracts.DTO.Image;
using ServiceContracts;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFormImageService _formImageUploader;
        private readonly IProductService _productManager;
        private readonly IFeatureService _featureManager;
        private readonly IProductTypeService _productTypeManager;
        public ProductsController
            (IWebHostEnvironment hostingEnvironment,
            IFormImageService formImageUploader,
            IProductService productManager,
            IFeatureService featureManager,
            IProductTypeService productTypeManager)
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
            return Json((await _featureManager.GetFeaturesAsync()).Where(x => x.ProductTypeId == id));
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View((await _productManager.GetProductsAsync()).Select(x => x.ToProductViewModel()));
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productManager.GetProductAsync(id.Value);

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
                id = x.FeatureId,
                name = x.Name + ": " + x.Value
            }), "id", "name");
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId,SelectedFeaturesIds,FormImages")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = product.ToProductAddRequest();
                if (product.FormImages != null && product.FormImages.Count > 0)
                {
                    foreach (var imageFile in product.FormImages)
                    {
                        if (imageFile.Length > 0)
                        {
                            string fileName = _formImageUploader.UploadImage(imageFile).Result;
                            dbProduct.Images.Add(new ImageAddRequest(fileName, product.Id));
                        }
                    }
                }
                foreach (var feature in product.SelectedFeaturesIds)
                {
                    dbProduct.Features.Add((await _featureManager.GetFeatureAsync(feature)).ToAddRequest());
                }
                await _productManager.CreateAsync(dbProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), product.ProductTypeId);
            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == product.ProductTypeId).FirstOrDefault();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.FeatureId,
                name = x.Name + ": " + x.Value
            }), "id", "name");
            return View(product);
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

            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), product.TypeResponse.Id);

            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == product.TypeResponse.Id).FirstOrDefault();

            var selectListItems = product.Features.Where(x => x.ProductTypeId == product.TypeResponse.Id);

            ViewData["SelectedFeatures"] = new SelectList(selectListItems.Select(x => new
            {
                id = x.FeatureId,
                name = x.Name + ": " + x.Value
            }), "id", "name", product.Features.Select(x => x.FeatureId).ToArray());

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Except(selectListItems).Select(x => new
            {
                id = x.FeatureId,
                name = x.Name + ": " + x.Value
            }), "id", "name");


            return View(product.ToProductViewModel());
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId,SelectedFeaturesIds")] ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ProductUpdateRequest updatedProduct = product.ToProductUpdateRequest();
                updatedProduct.Features.Clear();
                foreach (var feature in product.SelectedFeaturesIds)
                {
                    updatedProduct.Features.Add(await _featureManager.GetFeatureAsync(feature));
                }
                await _productManager.UpdateAsync(updatedProduct);
                return RedirectToAction(nameof(Index));
            }

            ProductResponse dbProduct = await _productManager.GetProductAsync(product.Id);

            ViewData["ProductTypeId"] = new SelectList(await _productTypeManager.GetProductTypesAsync(), nameof(ProductType.Id), nameof(ProductType.Value), dbProduct.TypeResponse.Id);

            var defaultType = (await _productTypeManager.GetProductTypesAsync()).Where(x => x.Id == dbProduct.TypeResponse.Id).FirstOrDefault();

            ViewData["TypeFeatures"] = new SelectList(defaultType?.Features.Select(x => new
            {
                id = x.FeatureId,
                name = x.Name + ": " + x.Value
            }), "id", "name");

            ViewData["SelectedFeatures"] = new SelectList((await _featureManager.GetFeaturesAsync()).Where(x => x.ProductTypeId == dbProduct.TypeResponse.Id).Select(x => new
            {
                id = x.FeatureId,
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
            await _productManager.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
