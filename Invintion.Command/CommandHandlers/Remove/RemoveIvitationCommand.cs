using MediatR;

namespace Invitation.Command.CommandHandlers.Remove
{
    public record RemoveIvitationCommand
   (
       string accountId,
       string subscriptionId,
       string UserId,
       string MemberId
   ) : IRequest<string>;
}
