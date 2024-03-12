using Invitation.Command.Domain.Records;
using MediatR;

namespace Invitation.Command.CommandHandlers.Join
{
    public record JoinInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId,
        List<Permission> Permissions
    ) : IRequest<string>;
}