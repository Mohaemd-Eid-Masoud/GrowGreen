using System;
using System.Threading.Tasks;

namespace GrowGreen.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        ICartRepository CartItems { get; }
        Task<int> SaveChangesAsync();
    }
}
