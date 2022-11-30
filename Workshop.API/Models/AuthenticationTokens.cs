using FluentValidation;

namespace Workshop.API.Models
{
    public class AuthenticationTokens
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }

        public class AuthenticationTokensRequestValidator : AbstractValidator<AuthenticationTokens>
        {
            public AuthenticationTokensRequestValidator()
            {
                RuleFor(x => x.Token)
                    .NotEmpty();
                RuleFor(x => x.RefreshToken)
                    .NotEmpty();
            }
        }
    }
}
