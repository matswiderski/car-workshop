using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Data;
using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public class CarRepositoryService : ICarRepositoryService
    {
        private readonly WorkshopDbContext _dbContext;
        public CarRepositoryService(UserManager<WorkshopUser> userManager,
            WorkshopDbContext dbContext,
            ITokenService tokenService)
        {
            _dbContext = dbContext;
        }

        public async Task<Car?> GetCarAsync(string id)
            => await _dbContext.Cars.Where(c => c.Id == id).SingleOrDefaultAsync();

        public async Task<IEnumerable<Car>> GetCarsAsync(string id)
           => await _dbContext.Cars.Where(c => c.PersonalUserId == id).ToListAsync();

        public async Task<Car> CreateCarAsync(CarDto car, WorkshopUser user)
        {
            Car newCar = new Car
            {
                Id = Guid.NewGuid().ToString(),
                Brand = car.brand,
                Model = car.model,
                LicensePlate = car.licensePlate,
                ProductionYear = car.productionYear,
                PersonalUserId = user.Id
            };
            await _dbContext.Cars.AddAsync(newCar);
            return newCar;
        }

        public async Task<Car?> UpdateCarAsync(CarDto carToUpdate)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == carToUpdate.id);
            if (car == null)
                return car;
            car.LicensePlate = carToUpdate.licensePlate;
            car.ProductionYear = carToUpdate.productionYear;
            car.Model = carToUpdate.model;
            car.Brand = carToUpdate.brand;
            return car;
        }

        public async Task<bool> DeleteCarAsync(string id)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == id);
            if (car == null)
                return false;
            _dbContext.Remove(car);
            return true;
        }
    }
}
