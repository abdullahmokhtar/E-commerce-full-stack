namespace Backend.Repository.Interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IReadOnlyList<T>> GetAll(params string[] includeProperties);
        Task<T> GetById(int id, params string[] includeProperties);
    }
}
