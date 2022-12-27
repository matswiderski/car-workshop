using FluentValidation;
using Workshop.API.Services;

namespace Workshop.API.Models
{
    public class AuthenticationRequest
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
    }

    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        private readonly IUserRepositoryService _userRepositoryService;
        public AuthenticationRequestValidator(IUserRepositoryService userRepositoryService)
        {
            _userRepositoryService = userRepositoryService;
            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync((x, cancellation) => UserExistsAsync(x))
                .WithMessage("User with the given e-mail address and password does not exist.")
                .OverridePropertyName("login");
        }

        private async Task<bool> UserExistsAsync(AuthenticationRequest credentials)
            => await _userRepositoryService.IsUserValidAsync(credentials);
    }
}
