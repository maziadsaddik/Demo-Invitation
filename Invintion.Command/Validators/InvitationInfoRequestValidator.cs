using FluentValidation;

namespace Invitation.Command.Validators
{
    public class InvitationInfoRequestValidator : AbstractValidator<InvitationInfoRequest>
    {
        public InvitationInfoRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.MemberId).NotEmpty();
            RuleFor(x => x.SubscriptionId).NotEmpty();
        }
    }
}
