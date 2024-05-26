namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoriesController(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SubCategory>>> GetAll()
            => Ok(await _subCategoryRepository.GetAll());

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<SubCategory>> GetById(int id)
        {
            var subCategory = await _subCategoryRepository.GetById(id);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }
    }
}
