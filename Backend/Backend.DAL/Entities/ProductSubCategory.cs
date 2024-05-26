namespace Backend.DAL.Entities
{
    public class ProductSubCategory
    {
        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public Product Product { get; set; } = null!;
        public SubCategory SubCategory { get; set; } = null!;

    }
}
