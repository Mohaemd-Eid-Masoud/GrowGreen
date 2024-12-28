using GrowGreen.Domain.Entities;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowGreen.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems
                .Include(c => c.Product) // Include Product details if needed
                .ToListAsync();
        }

        public async Task<CartItem> GetByUserIdAsync(int id)
        {
            return await _context.CartItems
                .Include(c => c.Product) // Include Product details if needed
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CartItem> GetByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task AddAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync(); // Save changes after adding
        }

        public async Task DeleteAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync(); // Save changes after deleting
            }
        }

        // Async Update method to allow asynchronous updates
        public async Task UpdateAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync(); // Save changes asynchronously
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
        return await _context.CartItems.FindAsync(id);
        }
    }
}
