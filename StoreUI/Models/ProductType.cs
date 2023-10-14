namespace StoreUI.Models
{
    public sealed class ProductType
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public ICollection<Product> Products { get; set;} = new HashSet<Product>();
    }
}
