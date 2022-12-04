using FluentValidation;
using Workshop.API.Extensions;

namespace Workshop.API.Models
{
    public class RegisterRequest
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
        public string ConfirmPasword { get; init; }
        public string AccountType { get; init; }
    }
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Password();

            RuleFor(x => x.ConfirmPasword)
                .Equal(x => x.Password).WithMessage("Passwords are not equal.");

            RuleFor(x => x.AccountType)
                .NotEmpty();
        }
    }
}
