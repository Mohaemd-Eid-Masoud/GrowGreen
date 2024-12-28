using GrowGreen.Application.DTOs;
using GrowGreen.Application.Services;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrowGreen.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ICartRepository _cartRepository;


        public CartController(ICartRepository cartRepository,CartService cartService)
        {
            cartRepository = _cartRepository;
            _cartService = cartService;
        }

        // Display cart items
        public async Task<IEnumerable<CartDto>> GetCartItemsAsync(int userId)
        {
            // Fetch a single cart item for the specified user
            var cartItem = await _cartRepository.GetByUserIdAsync(userId);  // Single CartItem

            // If there are no items, return an empty collection
            if (cartItem == null)
            {
                return Enumerable.Empty<CartDto>();  // Return empty list
            }

            // Map to DTO if necessary
            var cartItemDtos = new List<CartDto>{
                new CartDto
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.Quantity * cartItem.Product.Price,
                    UserId = cartItem.UserId,
                }
            };

            return cartItemDtos;
        }



        // Add an item to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            await _cartService.AddItemToCartAsync(userId, productId, quantity);
            return RedirectToAction(nameof(Index), new { userId });
        }

        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            await _cartService.RemoveItemFromCartAsync(userId, productId);
            return RedirectToAction(nameof(Index), new { userId });
        }

        // Proceed to checkout
        public async Task<IActionResult> Checkout(int userId)
        {
            var cartTotal = await _cartService.GetCartTotalAsync(userId);
            return View(cartTotal);
        }
    }
}
