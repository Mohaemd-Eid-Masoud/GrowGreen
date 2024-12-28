using GrowGreen.Domain.Common;
using GrowGreen.Domain.Entities;

namespace GrowGreen.Domain.Events
{
    public class OrderPlacedEvent : DomainEvent
    {
        public Order Order { get; }

        public OrderPlacedEvent(Order order)
        {
            Order = order;
        }
    }
}
