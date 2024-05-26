namespace Backend.API.Dtos.Orders
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureURL { get; set; } = string.Empty;
        public int OrderId { get; set; }
    }
}
