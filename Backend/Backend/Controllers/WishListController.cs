namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public WishListController(IWishListRepository wishListRepository
            , UserManager<AppUser> userManager
            , IProductRepository productRepository,
            IMapper mapper)
        {
            _wishListRepository = wishListRepository;
            _userManager = userManager;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll()
        {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);

            return  Ok(_mapper.Map<IReadOnlyList<ProductDto>>(await _wishListRepository.GetAllByUserId(user.Id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWishList(ProductIdtDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Product Id");
            
            var product = await _productRepository.GetById(model.ProductId);
            if (product is null)
                return NotFound("Product Not Found");

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var wishList = new WishList
            {
                AppUserId = user.Id,
                ProductId = product.Id,
            };

            var isAdded = await _wishListRepository.AddToList(wishList);

            if (!isAdded)
                return StatusCode(500, "Something went wrong");
            return Ok("Product Is Added successfuly");
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteFromList(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid Product Id");

            var product = await _productRepository.GetById(productId);
            if (product is null)
                return NotFound("Product Not Found");

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var wishList = await _wishListRepository.GetByUserIdAndProductId(user.Id, product.Id);

            if (wishList is null)
                return BadRequest("This Product Not in your wishList");
            
            var isDeleted = await _wishListRepository.DeleteFromList(wishList);
            if (!isDeleted)
                return StatusCode(500, "Somthing went wrong");
            return Ok("Product Is Deleted Successfully");
        }
        
    }
}
