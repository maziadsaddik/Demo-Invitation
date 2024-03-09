namespace Invitation.Command.Events
{
    public record InvitationAccepted(
          string AggregateId,
          int Sequence,
          DateTime DateTime,
          InvitationAcceptedData Data,
          int Version
      ) : Event<InvitationAcceptedData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationAcceptedData(
    string UserId,
    string? SubscriptionId,
    string MemberId,
    string AccountId
    );
}
