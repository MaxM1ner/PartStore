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
using ServiceContracts.DTO.ProductType;

namespace StoreUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class FeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFeatureService _featureService;
        private readonly IProductTypeService _productTypeService;

        public FeaturesController(ApplicationDbContext context, IFeatureService featureService, IProductTypeService productTypeService)
        {
            _context = context;
            _featureService = featureService;
            _productTypeService = productTypeService;
        }

        // GET: Admin/Features
        public async Task<IActionResult> Index()
        {
            var features = await _featureService.GetFeaturesAsync();
            return features.Count > 0 ?
                        View(features.Select(x => x.ToFeatureViewModel())) :
                        Problem("Entity set 'ApplicationDbContext.Features'  is null.");
        }

        // GET: Admin/Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !await _featureService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var feature = await _featureService.GetFeatureAsync(id.Value);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature.ToFeatureViewModel());
        }

        // GET: Admin/Features/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetProductTypesAsync(), nameof(ProductTypeResponse.Id), nameof(ProductTypeResponse.Value));
            return View();
        }

        // POST: Admin/Features/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,ProductTypeId")] FeatureViewModel feature)
        {
            if (ModelState.IsValid)
            {
                var dbFeature = feature.ToFeatureAddRequest();
                await _featureService.CreateAsync(dbFeature);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetProductTypesAsync(), nameof(ProductTypeResponse.Id), nameof(ProductTypeResponse.Value));
            return View(feature);
        }

        // GET: Admin/Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !await _featureService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var feature = await _featureService.GetFeatureAsync(id.Value);
            if (feature == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetProductTypesAsync(), nameof(ProductTypeResponse.Id), nameof(ProductTypeResponse.Value));
            return View(feature.ToFeatureViewModel());
        }

        // POST: Admin/Features/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value,ProductTypeId,FeatureId")] FeatureViewModel feature)
        {
            if (id != feature.FeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _featureService.EditAsync(feature.ToFeatureUpdateRequest());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _featureService.IsExistAsync(feature.FeatureId))
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
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetProductTypesAsync(), nameof(ProductTypeResponse.Id), nameof(ProductTypeResponse.Value));
            return View(feature);
        }

        // GET: Admin/Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await _featureService.IsExistAsync(id.Value))
            {
                return NotFound();
            }

            var feature = await _featureService.GetFeatureAsync(id.Value);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature.ToFeatureViewModel());
        }

        // POST: Admin/Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feature = await _featureService.GetFeatureAsync(id);
            if (feature != null)
            {
                await _featureService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
