using Backend.API.Dtos.Orders;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public OrdersController(UserManager<AppUser> userManager, 
            IConfiguration configuration,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll()
            => Ok(_mapper.Map<IReadOnlyList<OrderDto>>(await _orderRepository.GetAll()));

        [HttpGet("User")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAllUserOrders()
        {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);

            var userOrders = await _orderRepository.GetAllUserOrder(user.Id);

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(userOrders));
        }


        [HttpPost("checkoutsession")]
        public async Task<IActionResult> CheckoutSession(ShippingAddressDto model,[FromQuery] string url)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);

            var cart = await _cartRepository.GetCart(user.Id);

            if (cart is null)
                return BadRequest("There is no product in cart please add some");

            var orderItems = new List<OrderItem>();
            
            foreach (var item in cart.CartProducts)
            {
                orderItems.Add(new OrderItem
                {
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    ProductItemId = item.ProductId,
                    ProductName = item.Product.Name,
                    ProductPictureURL = item.Product.ImageCover
                });
            }

            var mappedShippingAddress = _mapper.Map<ShippingAddress>(model);

            var newOrder = new Order
            {
                AppUserId = user.Id,
                OrderItems = orderItems,
                TotalOrderPrice = cart.TotalCartPrice,
                ShippingAddress = mappedShippingAddress
            };

            var isCreated = await _orderRepository.CreateOrder(newOrder);

            if (!isCreated)
                return StatusCode(500, "Something went wrong");

            var isCratDeleted = await _cartRepository.DeleteCart(cart);

            if (!isCratDeleted)
                return StatusCode(500, "Something went wrong");

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var orderItem in orderItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "EGP",
                        UnitAmount = (long)orderItem.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = orderItem.ProductName,
                        }
                    },
                    Quantity = orderItem.Quantity,
                });
            }

            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = url,
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(session.Url);
        }

    }
}
