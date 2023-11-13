using Entities.Models;

namespace StoreUI.ViewModels
{
    public sealed class CatalogViewModel
    {
        public IEnumerable<ProductType>? ProductTypes { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
