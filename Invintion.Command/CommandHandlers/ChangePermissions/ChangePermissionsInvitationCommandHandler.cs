using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.CommandHandlers.Join;
using Invitation.Command.Domain;
using Invitation.Command.Exceptions;
using MediatR;

namespace Invitation.Command.CommandHandlers.ChangePermissions
{
    public class ChangePermissionsInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<ChangePermissionsInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(ChangePermissionsInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetStreamAsync($"{command.MemberId}-{command.subscriptionId}");
            if (events.Count == 0)
                throw new NotFoundException("Not Found Invitation");
            var invitation = Invitations.LoadFromHistory(events);
             invitation.ChangePermissionInvitation(command);
            await _eventStore.CommitAsync(invitation, cancellationToken);
            return invitation.Id;
        }
    }
}
