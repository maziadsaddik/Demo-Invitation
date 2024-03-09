using Invitation.Command.Events;

namespace Invitation.Command.Abstractions.Domain
{
    public interface IAggregate
    {
        string Id { get; }
        int Sequence { get; }
        IReadOnlyList<Event> GetUncommittedEvents();
        void MarkChangesAsCommitted();
    }
}
