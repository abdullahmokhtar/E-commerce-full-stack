namespace Backend.API.Dtos
{
    public class ProductImagesResolver : IValueResolver<Product, ProductDto, string[]>
    {
        private readonly IConfiguration _configuration;

        public ProductImagesResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string[] Resolve(Product source, ProductDto destination, string[] destMember, ResolutionContext context)
        {
            for (var i =0; i < source.Images.Length; i++)
            {
            if (!string.IsNullOrEmpty(source.Images[i]))
                {
                    Console.WriteLine(source.Images[i]);
                    source.Images[i] = _configuration["BaseUrl"] + "images/products/" + source.Images[i];
                    Console.WriteLine(source.Images[i]);
                }

            }
            return source.Images;
        }
    }
}
