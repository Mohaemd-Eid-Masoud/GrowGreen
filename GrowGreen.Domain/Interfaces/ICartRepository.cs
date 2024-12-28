using GrowGreen.Domain.Entities;

namespace GrowGreen.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> GetByIdAsync(int id);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(int id);
        Task<CartItem> GetByUserIdAsync(int id);
        Task<CartItem> GetByUserIdAndProductIdAsync(int userId, int productId);
    }
}
