using Invitation.Command.Domain.Records;

namespace Invitation.Command.Events
{
    public record InvitationChangedPermission(
           string AggregateId,
           int Sequence,
           DateTime DateTime,
           InvitationChangedPermissionData Data,
           int Version
       ) : Event<InvitationChangedPermissionData>(AggregateId, Sequence, DateTime, Data, Version);

    public record InvitationChangedPermissionData(
    string UserId,
    string SubscriptionId,
    string MemberId,
    string AccountId,
    List<Permission> Permissions
    );
}
