namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll()
            => Ok(_mapper.Map<IReadOnlyList<CategoryDto>>(await _categoryRepository.GetAll()));

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category is null) 
                return NotFound("Category Not Found");
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet]
        [Route("{id:int}/SubCategories")]
        public async Task<ActionResult<IReadOnlyList<SubCategory>>> GetAllSubCategoriesFromCategoryId(int id)
            =>  Ok(await _categoryRepository.GetAllSubCategoriesFromCategoryId(id));
    }
}
