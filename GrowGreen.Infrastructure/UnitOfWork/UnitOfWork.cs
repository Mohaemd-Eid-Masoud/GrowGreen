using GrowGreen.Domain.Interfaces;
using GrowGreen.Infrastructure.DbContexts;

namespace GrowGreen.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IProductRepository Products { get; }
        public IUserRepository Users { get; }
        public IOrderRepository Orders { get; }
        public ICartRepository CartItems { get; }

        public UnitOfWork(
            AppDbContext context,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository)
        {
            _context = context;
            Products = productRepository;
            Users = userRepository;
            Orders = orderRepository;
            CartItems = cartRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
