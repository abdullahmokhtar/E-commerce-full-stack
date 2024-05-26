using Backend.API.Dtos.Account;

namespace Backend.API.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public bool isDelivered { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDataDto AppUser { get; set; } = null!;
    }
}
