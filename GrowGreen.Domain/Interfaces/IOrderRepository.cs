using GrowGreen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowGreen.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        void Update(Order order);
        Task DeleteAsync(int id);

        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task UpdateAsync(Order order);
    }
}
