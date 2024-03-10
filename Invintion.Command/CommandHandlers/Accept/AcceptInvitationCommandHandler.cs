using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Reject;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using MediatR;

namespace Invitation.Command.CommandHandlers.Accept
{
    public class AcceptInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<AcceptInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");

            if (events.Count == 0)
                throw new NotFoundException("Not Found Invitation");
            var invitation = Invitations.LoadFromHistory(events);
            invitation.AcceptInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);

            return invitation.Id;
        }
    }
}

