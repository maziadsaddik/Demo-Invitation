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
            
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");
            Invitations invitation = new Invitations();
            if (events.Count != 0)
            {
                invitation = Invitations.LoadFromHistory(events);
            }
            invitation.SendInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);
            return invitation.Id;
        }
    }
}
