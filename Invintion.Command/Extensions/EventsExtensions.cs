using Invitation.Command.CommandHandlers.Accept;
using Invitation.Command.CommandHandlers.Cancel;
using Invitation.Command.CommandHandlers.Reject;
using Invitation.Command.CommandHandlers.Send;
using Invitation.Command.Domain.Records;
using Invitation.Command.Events;

namespace Invitation.Command.Extensions
{
    public static class EventsExtensions
    {
        public static InvitationSended ToEvent(this SendInvitationCommand command, int Sequence) => new(
                AggregateId: $"{command.MemberId}-{command.subscriptionId}",
                Sequence: Sequence,
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
        public static InvitationAccepted ToAcceptedEvent(this AcceptInvitationCommand command,int Sequence) => new(
                AggregateId: $"{command.MemberId}-{command.subscriptionId}",
                Sequence: Sequence,
                DateTime: DateTime.UtcNow,
                Data: new InvitationAcceptedData(
                    UserId: command.UserId,
                    SubscriptionId: command.subscriptionId,
                    AccountId: command.accountId,
                    MemberId: command.MemberId
                ),
                Version: 1
            );

        public static InvitationCanceled ToEvent(this CancelInvitationCommand command, int Sequence) => new(
               AggregateId: $"{command.MemberId}-{command.subscriptionId}",
               Sequence: Sequence,
               DateTime: DateTime.UtcNow,
               Data: new InvitationCanceledData(
                   UserId: command.UserId,
                   SubscriptionId: command.subscriptionId,
                   AccountId: command.accountId,
                   MemberId: command.MemberId
               ),
               Version: 1
           );
        public static InvitationRejected ToEvent(this RejectInvitationCommand command, int Sequence) => new(
               AggregateId: $"{command.MemberId}-{command.subscriptionId}",
               Sequence: Sequence,
               DateTime: DateTime.UtcNow,
               Data: new InvitationRejectedData(
                   UserId: command.UserId,
                   SubscriptionId: command.subscriptionId,
                   AccountId: command.accountId,
                   MemberId: command.MemberId
               ),
               Version: 1
           );
    }
}
