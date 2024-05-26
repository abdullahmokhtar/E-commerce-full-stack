namespace Backend.DAL.Entities
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
