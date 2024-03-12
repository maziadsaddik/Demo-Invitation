namespace Invitation.Command.Events
{
    public record InvitationLeaved(
           string AggregateId,
           int Sequence,
           DateTime DateTime,
           InvitationLeavedData Data,
           int Version
       ) : Event<InvitationLeavedData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationLeavedData(
    string UserId,
    string SubscriptionId,
    string MemberId,
    string AccountId
    );
}
