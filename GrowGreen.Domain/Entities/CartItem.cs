using GrowGreen.Domain.Common;

namespace GrowGreen.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * Product.Price;
        public int UserId { get; set; }  
    }
}
