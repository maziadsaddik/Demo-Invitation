using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Accept;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using Invitation.Command.Infrastructure;
using MediatR;

namespace Invitation.Command.CommandHandlers.Join
{
    public class JoinInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<JoinInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(JoinInvitationCommand command, CancellationToken cancellationToken)
        {
            //var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");
            Invitations invitation = new Invitations();
            //if (events.Count != 0)
            //{
            //    invitation = Invitations.LoadFromHistory(events);
            //}
            invitation.JoinInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);
            return invitation.Id;
        }
    }
}
