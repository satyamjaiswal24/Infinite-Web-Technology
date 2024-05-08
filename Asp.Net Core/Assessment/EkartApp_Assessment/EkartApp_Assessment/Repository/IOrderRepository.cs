using EkartApp_Assessment.Models;

namespace EkartApp_Assessment.Repository
{
    public interface IOrderRepository
    {
        Task PlaceOrder(Order order);
        Task<Order> GetOrderDetails(int orderId);
        //Task<decimal> GetBill(int orderId);
        Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
       // Task<Customer> GetCustomerWithHighestOrder();
    }
}
