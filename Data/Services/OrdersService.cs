using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Models;
using System;

namespace ProjWebProgramming.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(Guid userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, Guid userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.MovieId,
                    OrderId = order.OrderId,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

    }
}
