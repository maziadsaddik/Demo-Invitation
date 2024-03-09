using Invitation.Command.Events;

namespace Invitation.Command.Infrastructure.database
{
    public class OutboxMessage
    {
        private OutboxMessage(int id)
        {
            Id = id;
        }

        public OutboxMessage(Event @event)
        {
            Event = @event;
        }

        public int Id { get; private set; }
        public Event? Event { get; private set; }
    }
}
