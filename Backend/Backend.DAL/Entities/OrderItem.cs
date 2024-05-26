using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DAL.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductPictureURL { get; set; } = string.Empty;
        public int OrderId { get; set; }
    }
}
