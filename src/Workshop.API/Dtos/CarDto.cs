using FluentValidation;
using Workshop.API.Models;

namespace Workshop.API.Dtos
{
    public record CarDto(string? id, string brand, string model, string licensePlate, int productionYear);

    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(x => x.licensePlate)
                .NotEmpty()
                .MaximumLength(7);

            RuleFor(x => x.brand)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.model)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.productionYear)
                .InclusiveBetween(1950, DateTime.UtcNow.Year);
        }
    }
}
