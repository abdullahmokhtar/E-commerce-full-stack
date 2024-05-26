namespace Backend.API.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalCartPrice { get; set; }
        public List<CartProductDto> CartProducts { get; set; } = [];
        public string AppUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
