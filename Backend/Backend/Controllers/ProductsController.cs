namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<ProductDto>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok( _mapper.Map<PagedResponseDto<ProductDto>>(await _unitOfWork.ProductRepository.GetAll(queryObject, "Brand","Category")));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id, "Brand", "Category");
            if (product is null)
                return NotFound("Product Not Found");
            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
