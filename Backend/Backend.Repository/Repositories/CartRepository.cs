namespace Backend.BLL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> AddProductToCart(int productId, string userId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return null;

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == userId);

            if (cart == null)
            {
                var newCart = new Cart
                {
                    AppUserId = userId,
                    CartProducts = new List<CartProduct>
                    {
                        new CartProduct { ProductId = product.Id, Quantity = 1 }
                    },
                    TotalCartPrice = product.Price
                };
                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
                return newCart;
            }

            var newCartProduct = new CartProduct { 
                CartId = cart.Id, 
                ProductId = product.Id, 
                Quantity = 1,
            };
            
            await _context.CartProducts.AddAsync(newCartProduct);
            cart.TotalCartPrice += product.Price;
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Cart?> GetCart(string userId)
            => await _context.Carts
            .Where(e => e.AppUserId == userId)
            .Include(e => e.CartProducts)
            .ThenInclude(e => e.Product)
            .ThenInclude(e => e.Category)
            .FirstOrDefaultAsync();

        public async Task<bool> IsProductInYourCart(int productId, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == userId);
            if (cart == null)
                return false;
            var productCart = await _context.CartProducts
                .FirstOrDefaultAsync(e => e.CartId == cart.Id && e.ProductId == productId);
            return productCart != null;
        }

        public async Task<Cart?> RemoveProductFromCart(int productId, string userId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return null;

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == userId);

            if (cart == null) return null;

            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(e => e.CartId == cart.Id && e.ProductId == product.Id);
            
            if(cartProduct == null) return null;
            _context.CartProducts.Remove(cartProduct);
            cart.TotalCartPrice -= product.Price;
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> UpdateProductCount(int productId, string userId, int count)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) return false;

            var cart = await _context.Carts.FirstOrDefaultAsync(e => e.AppUserId == userId);

            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(e => e.ProductId == product.Id && e.CartId == cart.Id);

            if(cartProduct == null) return false;

            cartProduct.Quantity = count;

            var cartProducts = await _context.CartProducts.Where(e => e.CartId == cart.Id).Include(e => e.Product).ToListAsync();

            cart.TotalCartPrice = 0;
            foreach (var cp in cartProducts)
                cart.TotalCartPrice += cp.Product.Price * cp.Quantity;
            
            //cart.TotalCartPrice = cart.CartProducts.Sum(p => p.Quantity * p.Product.Price);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
