using FluentValidation;

namespace Bet.Application.Features.Bets.Commands.Checkout
{
    public class CheckoutBetCommandValidator : AbstractValidator<CheckoutBetCommand>
    {
        public CheckoutBetCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.JoiningFee)
                .NotEmpty().WithMessage("{JoiningFee} is required.")
                .GreaterThan(0).WithMessage("{JoiningFee} should be greater than zero.");
        }
    }
}
