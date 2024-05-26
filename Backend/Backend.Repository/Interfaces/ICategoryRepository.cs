namespace Backend.BLL.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<SubCategory>> GetAllSubCategoriesFromCategoryId(int categoryId);
    }
}
