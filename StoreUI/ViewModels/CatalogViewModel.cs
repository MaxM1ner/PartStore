using Entities.Models;
using Humanizer;
using ServiceContracts.DTO.Product;
using ServiceContracts.DTO.ProductType;

namespace StoreUI.ViewModels
{
    public sealed class CatalogViewModel
    {
        public bool IsRecommended { get; set; } = true;
        public IEnumerable<ProductTypeResponse>? ProductTypes { get; set; }
        public IEnumerable<ProductResponse>? Products { get; set; }
    }
}
