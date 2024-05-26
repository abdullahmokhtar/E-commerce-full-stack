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
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll()
            => Ok( _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository.GetAll("Brand","Category")));

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
