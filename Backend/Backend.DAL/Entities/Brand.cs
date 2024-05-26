namespace Backend.DAL.Entities
{
    public class Brand : BaseEntity
    {
        public string Image { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = [];
    }
}
