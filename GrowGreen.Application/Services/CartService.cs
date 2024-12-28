using GrowGreen.Domain.Entities;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Application.DTOs;

namespace GrowGreen.Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        // Add item to cart
        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found");

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            await _cartRepository.AddAsync(cartItem);
        }

        // Remove item from cart
        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var cartItem = await _cartRepository.GetByUserIdAndProductIdAsync(userId, productId);
            if (cartItem == null)
                throw new Exception("Item not found in the cart");

            await _cartRepository.DeleteAsync(cartItem.Id);
        }

        // Get cart total
        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            var cartItems = await _cartRepository.GetByUserIdAsync(userId);
            var total = cartItems.TotalPrice;
            return total;
        }
    }
}
