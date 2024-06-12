using Backend.DAL.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Backend.BLL.Repositories
{
    public class CategoryWith
    {
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
    }
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

        public async Task<List<CategoryWith>> GetOrdderItems()
        {
            var query = await (from c in _context.Categories
                        join p in _context.Products on c.Id equals p.CategoryId into cp
                        from p in cp.DefaultIfEmpty()
                        join o in _context.OrderItems on p.Id equals o.ProductItemId into op
                        from o in op.DefaultIfEmpty()
                        group new { c, p, o } by c.Name into grouped
                        select new CategoryWith
                        {
                            Name = grouped.Key,
                            TotalPrice = grouped.Sum(g => g.p != null ? g.p.Price : 0)
                        }).ToListAsync();
            return  query;

        }
    }
}
