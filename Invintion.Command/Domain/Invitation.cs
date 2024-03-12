using Invitation.Command.Abstractions.Domain;
using Invitation.Command.Domain.Records;
using Invitation.Command.Events;
using Microsoft.Identity.Client;
using Invitation.Command.Extensions;
using Invitation.Command.CommandHandlers.Send;
using MediatR;
using Invitation.Command.Exceptions;
using Microsoft.Extensions.Logging;
using Invitation.Command.CommandHandlers.Cancel;
using Invitation.Command.CommandHandlers.Accept;
using Invitation.Command.CommandHandlers.Reject;
using Invitation.Command.CommandHandlers.Join;
using Invitation.Command.CommandHandlers.ChangePermissions;
using Invitation.Command.CommandHandlers.Remove;
using Invitation.Command.CommandHandlers.Leave;

namespace Invitation.Command.Domain
{
    public class Invitations : Aggregate<Invitations>, IAggregate
    {

        
        public bool IsSended { get; private set; } = false;

        public bool IsAccepted { get; private set; } = false;  
        public bool CountInvitationSended { get; private set; } = false;
        public string AccountId { get; private set; } = string.Empty;
        public string MemberId { get; private set; } = string.Empty;
        public string UserId { get; private set; } = string.Empty;
        public string? SubscriptionId { get; private set; }
        public void SendInvitation(SendInvitationCommand command)
        {
            if (IsSended)
            {
                throw new RuleVaildationException("Invitation Is Sended");
            }
            if (!CountInvitationSended)
            {
                ApplyNewChange(command.ToEvent(1));
                return;
            }
            ApplyNewChange(command.ToEvent(NextSequence));
        }
        
        public void JoinInvitation(JoinInvitationCommand command)
        {
            if (!IsSended)
            {
                ApplyNewChange(command.ToEvent(1));
            }
            else 
            { 
                ApplyNewChange(command.ToEvent(NextSequence));
            }
        }
        public void CancelInvitation(CancelInvitationCommand command)
        {
            if (!IsSended)
            {
                throw new RuleVaildationException("Invitation not Sended");
            }
            ApplyNewChange(command.ToEvent(NextSequence));
        }
        public void AcceptInvitation(AcceptInvitationCommand command)
        {
            if (!IsSended)
            {
                throw new RuleVaildationException("Invitation not Sended");
            }
            ApplyNewChange(command.ToAcceptedEvent(NextSequence));

        }
         public void RejectInvitation(RejectInvitationCommand command)
        {
            if (!IsSended || IsAccepted)
            {
                throw new RuleVaildationException("Invitation not Sended");
            }
            ApplyNewChange(command.ToEvent(NextSequence));
            
        }
        public void ChangePermissionInvitation(ChangePermissionsInvitationCommand command)
        {
            if (!IsSended || !IsAccepted)
            {
                throw new RuleVaildationException("Invitation not Sended");
            }
            ApplyNewChange(command.ToEvent(NextSequence));
        }

        public void RemoveInvitation(RemoveIvitationCommand command)
        {
            if(!IsSended || !IsAccepted)
            {
                throw new RuleVaildationException("Invitation not Sended");
            }
            ApplyNewChange(command.ToEvent(NextSequence));
        }
        public void LeaveInvitation(LeaveInvitationCommand command)
        {
            if (!IsSended || !IsAccepted)
            {
                throw new RuleVaildationException("Invitation not Sended or Accepted");
            }
            ApplyNewChange(command.ToEvent(NextSequence));
        }
        protected override void Mutate(Event @event)
        {
            switch (@event)
            {
                case InvitationSended e:
                    Mutate(e);
                    break;
                case InvitationCanceled e:
                    Mutate(e);
                    break;
                case InvitationAccepted e:
                    Mutate(e);
                    break;
                case InvitationRejected e:
                    Mutate(e);
                    break;
                case InvitationJoined e:
                    Mutate(e);
                    break;
                case InvitationChangedPermission e:
                    Mutate(e);
                    break;
                case InvitationRemoved e:
                    Mutate(e);
                    break;
                case InvitationLeaved e:
                    Mutate(e);
                    break;
            }
        }

        public void Mutate(InvitationSended @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            List<Permission> permissions = new List<Permission>(@event.Data.Permissions.ToList());
            IsSended = true;
            CountInvitationSended= true;
        }

        public void Mutate(InvitationAccepted @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            IsSended = true;
            IsAccepted = true;
        }
        public void Mutate(InvitationRejected @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            IsSended = false;
        }

        public void Mutate(InvitationCanceled @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            IsSended = false;
        }
        public void Mutate(InvitationJoined @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            List<Permission> permissions = new List<Permission>(@event.Data.Permissions.ToList());
            IsSended = true;
            IsAccepted = true;
        }
        public void Mutate(InvitationChangedPermission @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            List<Permission> permissions = new List<Permission>(@event.Data.Permissions.ToList());
        }
        public void Mutate(InvitationRemoved @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            IsSended = false;
            IsAccepted = false;
        }
        public void Mutate(InvitationLeaved @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            IsSended = false;
            IsAccepted = false;
        }
    }
}
