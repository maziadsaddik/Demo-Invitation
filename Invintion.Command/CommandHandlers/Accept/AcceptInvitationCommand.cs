using MediatR;

namespace Invitation.Command.CommandHandlers.Accept
{
    public record AcceptInvitationCommand
    (
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId
    ) : IRequest<string>;
}
