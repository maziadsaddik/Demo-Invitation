using Invitation.Command.CommandHandlers.Send;
using Invitation.Command.Domain.Records;
using Invitation.Command.Events;

namespace Invitation.Command.Extensions
{
    public static class EventsExtensions
    {
        public static InvitationSended ToEvent(this SendInvitationCommand command) => new(
                AggregateId: $"{command.MemberId}-{command.subscriptionId}",
                Sequence: 1,
                DateTime: DateTime.UtcNow,
                Data: new InvitationSendedData(
                    UserId: command.UserId,
                    SubscriptionId:command.subscriptionId,
                    AccountId:command.accountId,
                    MemberId:command.MemberId,
                    Permissions:new List<Permission>(command.Permissions.ToList())
                ),
                Version: 1
            );
    }
}
