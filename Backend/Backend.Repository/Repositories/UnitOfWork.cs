namespace Backend.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            BrandRepository = new BrandRepository(context);
            CategoryRepository = new CategoryRepository(context);
            SubCategoryRepository = new SubCategoryRepository(context);
            ProductRepository = new ProductRepository(context);
        }
        public IBrandRepository BrandRepository { get; private set; }

        public ICategoryRepository CategoryRepository {  get; private set; }

        public ISubCategoryRepository SubCategoryRepository {  get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public async Task<int> Complete()
            => await _context.SaveChangesAsync();
    }
}
