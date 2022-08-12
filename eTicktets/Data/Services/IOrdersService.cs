using eTicktets.Models;

namespace eTicktets.Data.Services
{
    public interface IOrdersService
    {
        Task  storeOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task <List<Order>> GetOrderByUserIdAsync(string userId);
    }
}
