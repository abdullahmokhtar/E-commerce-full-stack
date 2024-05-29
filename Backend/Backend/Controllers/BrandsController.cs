using Backend.API.Dtos.Brands;
using Backend.BLL;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandsController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<BrandDto>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok( _mapper.Map<PagedResponseDto<BrandDto>>(await _brandRepository.GetAll(queryObject)));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BrandDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<BrandDto>> GetById(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
                return NotFound("Brand Not Found");
            return Ok(_mapper.Map<BrandDto>(brand));
        }
    }
}
