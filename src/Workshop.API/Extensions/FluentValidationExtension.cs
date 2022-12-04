using FluentValidation;

namespace Workshop.API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8, int maximumLength = 16)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Password is required.")
                .Length(minimumLength, maximumLength).WithMessage("Password must be between 8 and 16 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain uppercase letters.")
                .Matches("[a-z]").WithMessage("Password must contain lowercase letters.")
                .Matches("[0-9]").WithMessage("Password must contain numbers.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain special characters.");
            return options;
        }
    }
}
