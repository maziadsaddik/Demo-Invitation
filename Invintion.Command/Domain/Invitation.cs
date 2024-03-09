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

namespace Invitation.Command.Domain
{
    public class Invitations : Aggregate<Invitations>, IAggregate
    {
        public static Invitations SendInvitation(SendInvitationCommand command)
        {
            var Invitation = new Invitations();
            Invitation.ApplyNewChange(command.ToEvent());/*command.ToEvent()*/
            return Invitation;
        }


        //public bool IsSended { get; private set; } = false;
        //public int TotalFirstNameChangesToday { get; private set; }
        //public int TotalLastNameChangesToday { get; private set; }
        public string AccountId { get; private set; } = string.Empty;
        public string MemberId { get; private set; } = string.Empty;
        public string UserId { get; private set; } = string.Empty;
        public string? SubscriptionId { get; private set; }

        //public void checkSendInvitation()
        //{
        //    if (!IsSended)
        //    {
        //        throw new NotFoundException("Customer not found");
        //    }
        //}

        //public void ChangeName(ChangeCustomerNameCommand command)
        //{
        //    if (IsDeleted)
        //        throw new NotFoundException("Customer not found");

        //    if (TotalFirstNameChangesToday >= 3)
        //        throw new BusinessRuleViolationException("You can only change your first name 3 times per day");

        //    if (TotalLastNameChangesToday >= 5)
        //        throw new BusinessRuleViolationException("You can only change your last name 5 times per day");

        //    ApplyNewChange(command.ToEvent(NextSequence));
        //}

        //public void UpdateContactInfo(UpdateCustomerContactInfoCommand command)
        //{
        //    if (IsDeleted)
        //        throw new NotFoundException("Customer not found");

        //    ApplyNewChange(new CustomerContactInfoUpdated(
        //        AggregateId: command.Id,
        //        Sequence: Sequence + 1,
        //        DateTime: DateTime.UtcNow,
        //        Data: new CustomerContactInfoUpdatedData(
        //            Email: command.Email,
        //            Phone: command.Phone
        //        ),
        //        UserId: command.UserId,
        //        Version: 1
        //    ));
        //}
        public void CancelInvitation(CancelInvitationCommand command)
        {
            ApplyNewChange(command.ToEvent(NextSequence));
        }
        public void AcceptInvitation(AcceptInvitationCommand command)
        {
            ApplyNewChange(command.ToEvent(NextSequence));
        }
         public void RejectInvitation(RejectInvitationCommand command)
        {
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
            }
        }

        public void Mutate(InvitationSended @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
            List<Permission> permissions = new List<Permission>(@event.Data.Permissions.ToList());
        }

        //public void Mutate(CustomerNameChanged @event)
        //{
        //    if (@event.DateTime.Date == DateTime.UtcNow.Date)
        //    {
        //        if (FirstName != @event.Data.FirstName)
        //        {
        //            TotalFirstNameChangesToday++;
        //        }

        //        if (LastName != @event.Data.LastName)
        //        {
        //            TotalLastNameChangesToday++;
        //        }
        //    }
        //    else
        //    {
        //        TotalFirstNameChangesToday = 1;
        //        TotalLastNameChangesToday = 1;
        //    }

        //    FirstName = @event.Data.FirstName;
        //    LastName = @event.Data.LastName;
        //}

        public void Mutate(InvitationAccepted @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
        }
        public void Mutate(InvitationRejected @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
        }

        public void Mutate(InvitationCanceled @event)
        {
            AccountId = @event.Data.AccountId;
            MemberId = @event.Data.MemberId;
            UserId = @event.Data.UserId;
            SubscriptionId = @event.Data.SubscriptionId;
        }
    }
}
