using Backend.API.Dtos.Account;
using Backend.API.Dtos.Brands;
using Backend.API.Dtos.Orders;
using Backend.BLL;

namespace Backend.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<BrandUrlResolver>());

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<CategoryUrlResolver>());

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom<ProductCoverUrlResolver>())
                .ForMember(dest => dest.Images, opt => opt.MapFrom<ProductImagesResolver>());

            CreateMap<AppUser, UserDataDto>();

            CreateMap<Cart, CartDto>();
            CreateMap<CartProduct, CartProductDto>();

            CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDto<>));

            CreateMap<ShippingAddressDto, ShippingAddress>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

           
        }
    }
}
