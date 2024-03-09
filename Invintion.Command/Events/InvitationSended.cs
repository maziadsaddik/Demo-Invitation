using Invitation.Command.Domain.Records;
using System.Net;

namespace Invitation.Command.Events
{

        public record InvitationSended(
            string AggregateId,
            int Sequence,
            DateTime DateTime,
            InvitationSendedData Data,
            int Version
        ) : Event<InvitationSendedData>(AggregateId, Sequence, DateTime, Data, Version);

        public record InvitationSendedData(
        string UserId,
        string? SubscriptionId,
        string MemberId,
        string AccountId ,
        List<Permission> Permissions
        );
}
