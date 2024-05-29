namespace Backend.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<IReadOnlyList<T>> GetAll(QueryObject queryObject, params string[] includeProperties)
        public async Task<PagedResponse<T>> GetAll(QueryObject queryObject, params string[] includeProperties)
        {
            var totalRecords = await _context.Set<T>().AsNoTracking().CountAsync();

            var query = _context.Set<T>().AsNoTracking();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            var data = await query.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();

            var pagedResponse = new PagedResponse<T>(
                queryObject.PageNumber, 
                queryObject.PageSize, 
                totalRecords, 
                data);

            return pagedResponse;
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
