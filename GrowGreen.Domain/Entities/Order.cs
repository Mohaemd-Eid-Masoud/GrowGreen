using GrowGreen.Domain.Common;

namespace GrowGreen.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public Product Products { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<CartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
