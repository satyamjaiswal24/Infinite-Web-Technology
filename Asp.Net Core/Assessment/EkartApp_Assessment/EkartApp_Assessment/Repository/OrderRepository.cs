using EkartApp_Assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace EkartApp_Assessment.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext context;

        public OrderRepository(NorthwindContext dbContext)
        {
            context = dbContext;
        }

        public async Task PlaceOrder(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderDetails(int orderId)
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }


        public async Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return await context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
        }

       
    }
}
