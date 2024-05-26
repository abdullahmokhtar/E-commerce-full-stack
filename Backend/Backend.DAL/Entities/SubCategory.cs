namespace Backend.DAL.Entities
{
    public class SubCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProductSubCategory> ProductSubCategories { get; } = [];
    }
}
