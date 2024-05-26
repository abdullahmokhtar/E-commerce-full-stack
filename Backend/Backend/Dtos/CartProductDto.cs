namespace Backend.API.Dtos
{
    public class CartProductDto
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; } = null!;
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
