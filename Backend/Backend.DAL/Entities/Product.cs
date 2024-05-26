using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DAL.Entities
{
    public class Product : BaseEntity
    {
        public int Sold { get; set; }
        public string[] Images { get; set; } = [];
        public List<ProductSubCategory> ProductSubCategories { get; } = [];
        public int RatingsQuantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        public string ImageCover { get; set; } = string.Empty;
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public Brand Brand { get; set; } = null!;
        public int BrandId { get; set;}
        [Column(TypeName = "decimal(4,2)")]
        public decimal RatingsAverage { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}
