namespace Backend.API.Dtos.Brands
{
    public class BrandUrlResolver : IValueResolver<Brand, BrandDto, string>
    {
        private readonly IConfiguration _configuration;

        public BrandUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Brand source, BrandDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
                return _configuration["BaseUrl"] + "images/brands/" + source.Image;
            return null;
        }
    }
}
