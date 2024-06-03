using Backend.API.Dtos.Brands;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<BrandDto>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok( _mapper.Map<PagedResponseDto<BrandDto>>(await _unitOfWork.BrandRepository.GetAll(queryObject)));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BrandDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<BrandDto>> GetById(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetById(id);
            if (brand == null)
                return NotFound("Brand Not Found");
            return Ok(_mapper.Map<BrandDto>(brand));
        }
    }
}
