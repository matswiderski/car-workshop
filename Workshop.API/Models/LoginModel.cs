using FluentValidation;
using System;
using Workshop.API.Models;

namespace Workshop.API.Models
{
    public class LoginModel
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
public class LoginValidator : AbstractValidator<LoginModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
