namespace Backend.BLL.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _context;

        public WishListRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToList(WishList wishList)
        {
            await _context.WishLists.AddAsync(wishList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFromList(WishList wishList)
        {
            _context.WishLists.Remove(wishList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<Product>> GetAllByUserId(string userId)
            => await _context.WishLists
            .Where(e => e.AppUserId == userId)
            .Include(e =>e.Product)
            .ThenInclude(e =>e.Category)
            .Include(e => e.Product)
            .ThenInclude(e=> e.Brand)
            .Select(e=> e.Product).ToListAsync();

        public async Task<WishList?> GetByUserIdAndProductId(string userId, int productId)
            => await _context.WishLists.FirstOrDefaultAsync(e => e.AppUserId == userId && e.ProductId == productId);
    }
}
