namespace Backend.API.Dtos
{
    public class ProductIdtDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
