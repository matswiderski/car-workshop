using FluentValidation;
using System;
using Workshop.API.Models;

namespace Workshop.API.Models
{
    public class AuthenticationRequest
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
    }

    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
