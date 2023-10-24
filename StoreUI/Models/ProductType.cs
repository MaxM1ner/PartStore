namespace StoreUI.Models
{
    public class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
            this.Features = new HashSet<Feature>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public virtual ICollection<Feature> Features { get; private set; }
        public virtual ICollection<Product> Products { get; private set;} 
    }
}
