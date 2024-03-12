using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Cancel;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using MediatR;

namespace Invitation.Command.CommandHandlers.Remove
{
    public class RemoveInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<RemoveIvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(RemoveIvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");

            if (events.Count == 0)
                throw new NotFoundException("Not Found Invitation");
            var invitation = Invitations.LoadFromHistory(events);
            invitation.RemoveInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);

            return invitation.Id;
        }
    }
}
