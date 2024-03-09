using MediatR;

namespace Invitation.Command.CommandHandlers.Cancel
{
    public record CancelInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId
    ) : IRequest<string>;
}
