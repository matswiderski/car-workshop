using FluentValidation;
using Workshop.API.Extensions;
using Workshop.API.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Workshop.API.Models
{
    public class RegisterRequest
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string AccountType { get; init; }
    }
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IUserRepositoryService _repositoryService;
        public RegisterRequestValidator(IUserRepositoryService userRepositoryService)
        {
            _repositoryService = userRepositoryService;

            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .MustAsync((email, cancellation) => BeUniqueAsync(email)).WithMessage("Email is already taken.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Password();

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords are not equal.");

            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage("'Account Type' is required.");

        }
        private async Task<bool> BeUniqueAsync(string email)
        {
            var user = await _repositoryService.GetUserByEmailAsync(email);
            return user == null ? true : false;
        }

    }
}
