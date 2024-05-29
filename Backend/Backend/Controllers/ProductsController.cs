using Backend.BLL;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<ProductDto>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok( _mapper.Map<PagedResponseDto<ProductDto>>(await _productRepository.GetAll(queryObject, "Brand","Category")));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetById(id, "Brand", "Category");
            if (product == null)
                return NotFound("Product Not Found");
            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
