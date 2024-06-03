namespace Backend.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> Complete();
    }
}
