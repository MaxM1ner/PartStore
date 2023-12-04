using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.ViewModels
{
    public sealed class ProductViewModel
    {
        public ProductViewModel()
        {
            FeatureIds = new List<int>();
            CommentIds = new List<int>();
            ImageIds = new List<int>();
            OrderIds = new List<int>();
            BuildIds = new List<int>();
        }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public IEnumerable<int> FeatureIds { get; private set; }
        public IEnumerable<int> CommentIds { get; private set; }
        public IEnumerable<int> ImageIds { get; private set; }
        public IEnumerable<int> OrderIds { get; private set; }
        public IEnumerable<int> BuildIds { get; private set; }
    }
}
