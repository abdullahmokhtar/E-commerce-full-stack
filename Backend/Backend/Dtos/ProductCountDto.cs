namespace Backend.API.Dtos
{
    public class ProductCountDto
    {
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
