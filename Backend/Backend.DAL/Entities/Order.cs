using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DAL.Entities
{
    public enum OrderPaymentStatus { Pending, Received, Failed }
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public bool isDelivered { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrderPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; } = null!;

    }
}
