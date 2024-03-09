using MediatR;
using Invitation.Command.Domain;
using Invitation.Command.Infrastructure;
using Invitation.Command.Abstractions.Persistence;

namespace Invitation.Command.CommandHandlers.Send
{
    public class SendInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<SendInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(SendInvitationCommand command, CancellationToken cancellationToken)
        {
            var invitation = Invitations.SendInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);

            return invitation.Id;
        }
    }
}
