using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Send;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using Invitation.Command.Infrastructure;
using MediatR;

namespace Invitation.Command.CommandHandlers.Cancel
{
    public class CancelInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<CancelInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(CancelInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");

            if (events.Count == 0)
                throw new NotFoundException("Not Found Invitation");
            var invitation = Invitations.LoadFromHistory(events);
            invitation.CancelInvitation(command);
        
            await _eventStore.CommitAsync(invitation, cancellationToken);

            return invitation.Id;
        }
    }
}
