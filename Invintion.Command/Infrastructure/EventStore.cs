using Invitation.Command.Abstractions.Domain;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Events;
using Invitation.Command.Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Invitation.Command.Infrastructure
{
    public class EventStore(InvitationDbContext context) : IEventStore
    {
        private readonly InvitationDbContext _context = context;
        public Task AppendToStreamAsync(IAggregate aggregate)
        {
            throw new NotImplementedException();
        }

        public Task AppendToStreamAsync(Event @event)
        {
            throw new NotImplementedException();
        }

        public async Task CommitAsync(IAggregate aggregate, CancellationToken cancellationToken)
        {
            var events =aggregate.GetUncommittedEvents();

            var messages = events.Select(x => new OutboxMessage(x));

            await _context.Events.AddRangeAsync(events, cancellationToken);

            await _context.OutboxMessages.AddRangeAsync(messages, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Event?> GetLastEventByAggregateId(string aggregateId)
        {
           return  _context.Events
                   .Where(@event => @event.AggregateId == aggregateId)
                   .OrderBy(@event => @event.Sequence).LastOrDefaultAsync();
        }

        public Task<List<Event>> GetStreamAsync(string aggregateId)
        {
           return _context.Events
            .Where(x => x.AggregateId == aggregateId)
            .OrderBy(x => x.Id)
            .ToListAsync();
        }
    }
}
