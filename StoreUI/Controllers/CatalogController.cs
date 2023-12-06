using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using ServiceContracts;
using ServiceContracts.DTO.Product;
using StoreUI.ViewModels;

namespace StoreUI.Controllers
{
    public sealed class CatalogController : Controller
    {
        private readonly IProductService _productManager;
        private readonly IProductTypeService _productTypeManager;
        public CatalogController(IProductService productManager, IProductTypeService productTypeManager)
        {
            _productManager = productManager;
            _productTypeManager = productTypeManager;
        }
        public async Task<IActionResult> Index(int? typeid)
        {
            var catalogProducts = new List<ProductResponse>();
            var types = await _productTypeManager.GetProductTypesAsync();
            var products = await _productManager.GetProductsAsync();

            products = products.Where(x => x.Quantity > 0 && x.IsVisible).ToList();
            if (typeid != null) { catalogProducts.AddRange(products.Where(x => x.TypeResponse.Id == typeid).ToList()); }
            else
            {
                catalogProducts.AddRange(products);
            }

            var viewModel = new CatalogViewModel() { Products = catalogProducts, ProductTypes = types };
            if (typeid.HasValue) { viewModel.IsRecommended = false; }
            return View(viewModel);
        }
    }
}
