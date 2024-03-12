namespace Invitation.Command.Events
{
    public record InvitationRemoved(
         string AggregateId,
         int Sequence,
         DateTime DateTime,
         InvitationRemovedData Data,
         int Version
     ) : Event<InvitationRemovedData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationRemovedData(
    string UserId,
    string SubscriptionId,
    string MemberId,
    string AccountId
    );
}
