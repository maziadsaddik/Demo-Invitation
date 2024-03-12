using Invitation.Command.Domain.Records;

namespace Invitation.Command.Events
{
    public record InvitationJoined(
            string AggregateId,
            int Sequence,
            DateTime DateTime,
            InvitationJoinedData Data,
            int Version
        ) : Event<InvitationJoinedData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationJoinedData(
    string UserId,
    string SubscriptionId,
    string MemberId,
    string AccountId,
    List<Permission> Permissions
    );
}
