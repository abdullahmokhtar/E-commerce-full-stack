namespace Backend.BLL.Interfaces
{
    public interface IWishListRepository
    {
        Task<IReadOnlyList<Product>> GetAllByUserId(string userId);
        Task<WishList?> GetByUserIdAndProductId(string userId, int productId);
        Task<bool> AddToList(WishList wishList);
        Task<bool> DeleteFromList(WishList wishList);
    }
}
