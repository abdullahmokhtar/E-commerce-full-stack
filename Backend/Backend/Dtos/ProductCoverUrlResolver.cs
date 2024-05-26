namespace Backend.API.Dtos
{
    public class ProductCoverUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductCoverUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageCover))
                return _configuration["BaseUrl"] + "images/products/" + source.ImageCover;
            return null;
        }
    }
}
