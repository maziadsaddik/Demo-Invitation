using Invitation.Command.Abstractions.Domain;
using Invitation.Command.Domain.Records;
using Invitation.Command.Events;
using Microsoft.Identity.Client;
using Invitation.Command.Extensions;
using Invitation.Command.CommandHandlers.Send;

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

        //public bool IsDeleted { get; private set; }
        //public int TotalFirstNameChangesToday { get; private set; }
        //public int TotalLastNameChangesToday { get; private set; }
        public string AccountId { get; private set; } = string.Empty;
        public string MemberId { get; private set; } = string.Empty;
        public string UserId { get; private set; } = string.Empty;
        public string? SubscriptionId { get; private set; }

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

        protected override void Mutate(Event @event)
        {
            switch (@event)
            {
                case InvitationSended e:
                    Mutate(e);
                    break;
                //case CustomerNameChanged e:
                //    Mutate(e);
                //    break;
                //case CustomerContactInfoUpdated e:
                //    Mutate(e);
                //    break;
                //case CustomerDeleted e:
                //    Mutate(e);
                //    break;
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

        //public void Mutate(CustomerContactInfoUpdated @event)
        //{
        //    Email = @event.Data.Email;
        //    Phone = @event.Data.Phone;
        //}

        //public void Mutate(CustomerDeleted _)
        //{
        //    IsDeleted = true;
        //}
    }
}
