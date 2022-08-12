using eTicktets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicktets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext context;

        public OrdersService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
            var orders = await context.Orders.Include(n => n.OrderItems)
                                        .ThenInclude(n => n.Movie)
                                        .Where(n => n.UserId == userId).ToListAsync();
            
            return orders;
        }

        public async Task storeOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await context.AddAsync(order);
            await context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await context.OrderItems.AddAsync(orderItem);
            }
            await context.SaveChangesAsync();
        }
    }
}
