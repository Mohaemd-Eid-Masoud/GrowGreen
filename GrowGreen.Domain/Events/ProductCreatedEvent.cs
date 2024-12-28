using GrowGreen.Domain.Common;
using GrowGreen.Domain.Entities;

namespace GrowGreen.Domain.Events
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
