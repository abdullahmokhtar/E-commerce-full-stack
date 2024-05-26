namespace Backend.DAL.Entities
{
    public class WishList
    {
        public string AppUserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
