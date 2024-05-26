namespace Backend.DAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalCartPrice { get; set; }
        public List<CartProduct> CartProducts { get; set; } = [];
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
