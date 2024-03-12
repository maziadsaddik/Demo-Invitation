using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Remove;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using MediatR;

namespace Invitation.Command.CommandHandlers.Leave
{
    public class LeaveInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<LeaveInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(LeaveInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");

            if (events.Count == 0)
                throw new NotFoundException("Not Found Invitation");
            var invitation = Invitations.LoadFromHistory(events);
            invitation.LeaveInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);

            return invitation.Id;
        }
    }
}
