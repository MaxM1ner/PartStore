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
using StoreUI.Data;
using StoreUI.Data.Migrations;
using StoreUI.Models;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
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
            var products = _productManager.GetProducts();
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

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId")] Product product, ICollection<IFormFile> imageFiles, IEnumerable<int> selectedFeatures)
        {
            if (ModelState.IsValid)
            {
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        if (imageFile.Length > 0)
                        {
                            string fileName = _formImageUploader.UploadImage(imageFile).Result;
                            product.Images.Add(new ProductImage() 
                            {
                                Path = fileName, 
                                Product = product,
                                ProductId = product.Id
                            });
                        }
                    }
                }
                foreach (var feature in selectedFeatures)
                {
                    product.Features.Add(await _context.Features.Where(x => x.Id == feature).FirstAsync());
                }
                await _productManager.Create(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", product.ProductTypeId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_productManager.IsExist((int)id)) return NotFound();
            var product = _productManager.GetProduct((int)id);

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", product.ProductTypeId);
            ViewData["Features"] = new SelectList(
            _context.Features.Where(x => x.ProductTypeId == product.ProductTypeId).Select(x => new
            {
                x.Id,
                Name = x.Name + ": " + x.Value
            })
            , "Id", "Name", product.Features.Select(x => x.Id).ToArray());
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Description,Quantity,IsVisible,ProductTypeId")] Product product, IEnumerable<int> selectedFeatures)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                product.Features.Clear();
                foreach (var feature in selectedFeatures)
                {
                    product.Features.Add(await _context.Features.Where(x => x.Id == feature).FirstAsync());
                }

                await _productManager.Edit(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Value", product.ProductTypeId);
            ViewData["Features"] = new SelectList(
            _context.Features.Where(x => x.ProductTypeId == product.ProductTypeId).Select(x => new
            {
                x.Id,
                Name = x.Name + ": " + x.Value
            })
            , "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if(!_productManager.IsExist((int)id)) return NotFound();
            var product = _productManager.GetProduct((int)id);

            return View(product);
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
