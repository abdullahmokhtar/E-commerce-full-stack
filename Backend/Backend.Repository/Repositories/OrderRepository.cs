namespace Backend.BLL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<Order>> GetAll()
            => await _context.Orders.Include(o => o.OrderItems).ToListAsync();

        public async Task<IReadOnlyList<Order>> GetAllUserOrder(string userId)
            => await _context.Orders.Where(o => o.AppUserId == userId).Include(o => o.OrderItems).ToListAsync();

        public async Task<Order?> GetById(int id)
            => await _context.Orders.FindAsync(id);
    }
}
