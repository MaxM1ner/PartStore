using Entities.Models;
using ServiceContracts.DTO.Product;
using ServiceContracts.DTO.ProductType;

namespace StoreUI.ViewModels
{
    public sealed class CatalogViewModel
    {
        public IEnumerable<ProductTypeResponse>? ProductTypes { get; set; }
        public IEnumerable<ProductResponse>? Products { get; set; }
    }
}
