namespace Invitation.Command.Events
{
    public record InvitationRejected(
          string AggregateId,
          int Sequence,
          DateTime DateTime,
          InvitationRejectedData Data,
          int Version
      ) : Event<InvitationRejectedData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationRejectedData(
    string UserId,
    string SubscriptionId,
    string MemberId,
    string AccountId
    );

    
}
