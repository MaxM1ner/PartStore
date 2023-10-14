namespace StoreUI.Models
{
    public sealed class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public ICollection<Product> Products { get; private set;} 
    }
}
