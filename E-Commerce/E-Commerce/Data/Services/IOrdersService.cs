namespace E_Commerce.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> shoppingCartItems, string userId, string userEmail);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
