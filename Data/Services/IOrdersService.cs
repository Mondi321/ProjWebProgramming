using ProjWebProgramming.Models;

namespace ProjWebProgramming.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, Guid userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(Guid userId, string userRole);
    }
}
