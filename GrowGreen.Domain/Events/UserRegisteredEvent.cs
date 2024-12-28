using GrowGreen.Domain.Common;
using GrowGreen.Domain.Entities;

namespace GrowGreen.Domain.Events
{
    public class UserRegisteredEvent : DomainEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
