using GrowGreen.Application.DTOs;
using GrowGreen.Application.Services;
using GrowGreen.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrowGreen.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Display user's orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // View details of a specific order
        public async Task<IActionResult> Details(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // Place an order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int userId, List<int> productIds)
        {
            await _orderService.PlaceOrderAsync(userId,productIds);
            return RedirectToAction(nameof(Index));
        }
    }
}
