namespace Backend.DAL.Entities
{
    public class Category : BaseEntity
    {
        public string Image { get; set; } = string.Empty;
        public ICollection<SubCategory> SubCategories { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
    }
}
