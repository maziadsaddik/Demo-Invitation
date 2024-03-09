using Invitation.Command.Abstractions.Domain;
using Invitation.Command.Events;

namespace Invitation.Command.Abstractions.Persistence
{
    public interface IEventStore
    {
        Task AppendToStreamAsync(IAggregate aggregate);
        Task AppendToStreamAsync(Event @event);

        Task<List<Event>> GetStreamAsync(string aggregateId);

        Task CommitAsync(IAggregate aggregate, CancellationToken cancellationToken);
    }
}
