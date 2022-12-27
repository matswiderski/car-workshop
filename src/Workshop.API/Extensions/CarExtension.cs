using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class CarExtension
    {
        public static CarDto AsDto(this Car car)
        {
            return new CarDto(car.Brand, car.Model, car.LicensePlate, car.ProductionYear);
        }
    }
}
