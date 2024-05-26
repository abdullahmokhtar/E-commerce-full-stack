namespace Backend.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<SubCategory>> GetAllSubCategoriesFromCategoryId(int categoryId)
            => await _context.SubCategories.Where(sc => sc.CategoryId == categoryId).ToListAsync();
    }
}
