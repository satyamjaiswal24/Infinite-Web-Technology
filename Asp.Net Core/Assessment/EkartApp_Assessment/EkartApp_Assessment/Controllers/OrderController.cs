using EkartApp_Assessment.Models;
using EkartApp_Assessment.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EkartApp_Assessment.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            await _orderRepository.PlaceOrder(order);
            var updatedOrder = await _orderRepository.GetOrderDetails(order.OrderId);
            return View("PlaceOrder", updatedOrder);
        }

      
        [HttpGet]
        public async Task<IActionResult> orderDetails(int id)
        {
            var order = await _orderRepository.GetOrderDetails(id); // Await the async method
            return View(order);
        }


        [HttpGet]
        public async Task<IActionResult> OrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRange(startDate, endDate);
            return View(orders);
        }

    }
}
