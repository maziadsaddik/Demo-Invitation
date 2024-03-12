using MediatR;

namespace Invitation.Command.CommandHandlers.Leave
{
    public record LeaveInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId
    ) : IRequest<string>;
}
