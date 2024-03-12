using Invitation.Command.Domain.Records;
using MediatR;

namespace Invitation.Command.CommandHandlers.ChangePermissions
{ 
    public record ChangePermissionsInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId,
        List<Permission> Permissions
    ) : IRequest<string>;
}
