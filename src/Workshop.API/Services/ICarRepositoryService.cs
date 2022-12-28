using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface ICarRepositoryService
    {
        Task<Car> CreateCarAsync(CarDto car, WorkshopUser user);
        Task<bool> DeleteCarAsync(string id);
        Task<Car?> GetCarAsync(string id);
        Task<IEnumerable<Car>> GetCarsAsync(string id);
    }
}