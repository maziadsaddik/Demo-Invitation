using FluentValidation;

namespace Invitation.Command.Validators
{
    public class InvitationRequestValidator: AbstractValidator<InvitationRequest>
    {
        public InvitationRequestValidator()
        {
            RuleFor(x=>x.InvitationInfo.AccountId).NotEmpty();
            RuleFor(x => x.InvitationInfo.UserId).NotEmpty();
            RuleFor(x=>x.InvitationInfo.MemberId).NotEmpty();
            RuleFor(x=>x.InvitationInfo.SubscriptionId).NotEmpty();
            RuleFor(x => x.Permissions).NotEmpty();

        }
    }
}
