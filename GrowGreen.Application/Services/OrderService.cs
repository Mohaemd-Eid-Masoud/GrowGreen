using GrowGreen.Domain.Entities;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Application.DTOs;

namespace GrowGreen.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // Get all orders and return them as DTOs
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            // Map Order entities to OrderDto
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate
            });
        }

        // Get an order by its ID
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            // Map Order entity to OrderDto
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate
            };
        }

        // Place a new order
        public async Task PlaceOrderAsync(int userId, List<int> productIds)
        {
            // Validate the products and calculate total amount
            var products = new List<Product>();
            foreach (var productId in productIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new Exception($"Product with ID {productId} not found");

                products.Add(product);
            }

            var totalAmount = products.Sum(p => p.Price);

            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                Products = products
            };

            await _orderRepository.AddAsync(order);
        }

        // Cancel an order
        public async Task CancelOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            // Perform any business logic for cancellation
            order.Status = Domain.Common.OrderStatus.Cancelled;
            _orderRepository.Update(order);
        }
    }
}
