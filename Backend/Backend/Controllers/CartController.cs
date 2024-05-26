namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var cart = await _cartRepository.GetCart(user.Id);

            if (cart == null) 
                return NotFound();
            return Ok(_mapper.Map<CartDto>(cart));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(ProductIdtDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var isProductInCart = await _cartRepository.IsProductInYourCart(model.ProductId, user.Id);

            if (isProductInCart)
                return BadRequest("This product Is already in your cart");

            var cart = await _cartRepository.AddProductToCart(model.ProductId, user.Id);

            if(cart == null)
                return BadRequest();
            return Ok("Product Is added successfully");
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<Cart>> RemoveProductFromCart(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid product id");

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var cart = await _cartRepository.RemoveProductFromCart(productId, user.Id);

            if(cart == null)
                return NotFound("Product Not in Your Cart");
            return Ok(cart);
        }

        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateProductCount(int productId, ProductCountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var isUpdated = await _cartRepository.UpdateProductCount(productId, user.Id, model.Count);

            return isUpdated ? Ok("Product count in cart updated") :
                StatusCode(500, "Something went wrong while updating product count in cart");
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);

            var cart = await _cartRepository.GetCart(user.Id);
            if (cart is null)
                return BadRequest("There is no cart to be deleted");

            var isCratDeleted = await _cartRepository.DeleteCart(cart);

            if (!isCratDeleted)
                return StatusCode(500, "Something went wrong while deleting cart");
              
            return Ok("Cart Is celered successfully");


        }
    }
}
