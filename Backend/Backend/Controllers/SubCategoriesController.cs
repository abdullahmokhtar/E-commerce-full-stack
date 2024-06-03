namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SubCategory>>> GetAll([FromQuery] QueryObject queryObject)
            => Ok(await _unitOfWork.SubCategoryRepository.GetAll(queryObject));

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<SubCategory>> GetById(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetById(id);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }
    }
}
