namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<CategoryDto>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok(
                _mapper.Map<PagedResponseDto<CategoryDto>>
                (await _unitOfWork.CategoryRepository.GetAll(queryObject)));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category is null) 
                return NotFound("Category Not Found");
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet]
        [Route("{id:int}/SubCategories")]
        public async Task<ActionResult<IReadOnlyList<SubCategory>>> GetAllSubCategoriesFromCategoryId(int id)
            =>  Ok(await _unitOfWork.CategoryRepository.GetAllSubCategoriesFromCategoryId(id));
    }
}
