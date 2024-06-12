using Backend.BLL.Repositories;

namespace Backend.BLL.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<IReadOnlyList<Order>> GetAllUserOrder(string userId);
        Task<bool> CreateOrder(Order order);
        Task<List<CategoryWith>> GetOrdderItems();
    }
}
