namespace Backend.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAll(params string[] includeProperties)
        {

            var query = _context.Set<T>().AsNoTracking();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id, params string[] includeProperties)
        {
            var query = _context.Set<T>().Where(e => e.Id == id);

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty)
                );
            return await query.FirstOrDefaultAsync();
        }
    }
}
