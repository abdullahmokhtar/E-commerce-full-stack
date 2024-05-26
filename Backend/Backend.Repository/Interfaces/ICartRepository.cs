namespace Backend.BLL.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCart(string userId);
        Task<Cart> AddProductToCart(int productId, string userId);
        Task<Cart?> RemoveProductFromCart(int productId, string userId);
        Task<bool> IsProductInYourCart(int productId, string userId);
        Task<bool> UpdateProductCount(int productId, string userId, int count);
        Task<bool> DeleteCart(Cart cart);
    }
}
