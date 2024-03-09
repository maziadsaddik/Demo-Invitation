namespace Invitation.Command.Events
{
    public record InvitationCanceled(
         string AggregateId,
         int Sequence,
         DateTime DateTime,
         InvitationCanceledData Data,
         int Version
     ) : Event<InvitationCanceledData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationCanceledData(
    string UserId,
    string? SubscriptionId,
    string MemberId,
    string AccountId
    );
}
