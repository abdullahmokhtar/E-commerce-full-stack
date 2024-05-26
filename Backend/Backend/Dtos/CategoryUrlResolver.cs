namespace Backend.API.Dtos
{
    public class CategoryUrlResolver : IValueResolver<Category, CategoryDto, string>
    {
        private readonly IConfiguration _configuration;

        public CategoryUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Category source, CategoryDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
                return _configuration["BaseUrl"] + "images/categories/" + source.Image;
            return null;
        }
    }
}
