namespace StoreUI.Models
{
    public sealed class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
            this.Features = new HashSet<Feature>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public ICollection<Feature> Features { get; private set; }
        public ICollection<Product> Products { get; private set;} 
    }
}
