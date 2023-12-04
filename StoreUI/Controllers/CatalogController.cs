using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using ServiceContracts.DTO.Product;
using Services;
using StoreUI.ViewModels;

namespace StoreUI.Controllers
{
    public sealed class CatalogController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly ProductTypeManager _productTypeManager;
        public CatalogController(ProductManager productManager, ProductTypeManager productTypeManager)
        {
            _productManager = productManager;
            _productTypeManager = productTypeManager;
        }
        public async Task<IActionResult> Index(int? typeid)
        {
            var catalogProducts = new HashSet<ProductResponse>();
            var types = await _productTypeManager.GetProductTypesAsync();
            var products = await _productManager.GetProductsAsync();

            products = products.Where(x => x.Quantity > 0 && x.IsVisible).ToList();
            if (typeid != null) { catalogProducts.AddRange(products.Where(x => x.TypeResponse.Id == typeid).ToHashSet()); }
            else
            {
                catalogProducts.AddRange(products);
            }

            return View(new CatalogViewModel() { Products = catalogProducts, ProductTypes = types });
        }
    }
}
