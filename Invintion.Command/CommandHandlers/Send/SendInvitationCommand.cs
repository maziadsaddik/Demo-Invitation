using Invitation.Command.Domain.Records;
using MediatR;

namespace Invitation.Command.CommandHandlers.Send
{
    public record SendInvitationCommand(
        string accountId,
        string subscriptionId,
        string UserId,
        string MemberId,
        List<Permission> Permissions 
    ) : IRequest<string>;
}
