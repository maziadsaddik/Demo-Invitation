using MediatR;

namespace Invitation.Command.CommandHandlers.Reject
{
    public record RejectInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId
    ) : IRequest<string>;
}
