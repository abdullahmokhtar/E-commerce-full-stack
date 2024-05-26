using Backend.API.Dtos.Brands;

namespace Backend.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Sold { get; set; }
        public string[] Images { get; set; }
        //public List<ProductSubCategory> ProductSubCategories { get; } = [];
        public int RatingsQuantity { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageCover { get; set; }
        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }
        public BrandDto Brand { get; set; }
        public int BrandId { get; set; }
        public decimal RatingsAverage { get; set; }
    }
}
