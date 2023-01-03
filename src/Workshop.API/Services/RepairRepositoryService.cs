using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Workshop.API.Data;
using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public class RepairRepositoryService : IRepairRepositoryService
    {
        private readonly WorkshopDbContext _dbContext;
        public RepairRepositoryService(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Repair?> GetRepairAsync(string id)
            => await _dbContext.Repairs.Where(r => r.Id == id).SingleOrDefaultAsync();

        public IEnumerable<Repair> GetUserRepairs(string userId)
            => _dbContext.Repairs.Where(r => r.PersonalUserId == userId);

        public async Task<Repair> CreateRepairAsync(RepairDto repairDto, WorkshopUser user)
        {
            Car? car = await _dbContext.Cars.Where(c => c.Id == repairDto.car.id).SingleOrDefaultAsync();
            Models.Workshop workshop = await _dbContext.Workshops.Where(w => w.Id == repairDto.workshop.id).Include(w => w.Localization).SingleOrDefaultAsync();
            if (car == null || workshop == null)
                return null;
            Repair newRepair = new Repair
            {
                Id = Guid.NewGuid().ToString(),
                PersonalUserId = user.Id,
                CarId = repairDto.car.id,
                Car = car,
                WorkshopId = repairDto.workshop.id,
                Workshop = workshop,
                Message = repairDto.message,
            };
            ICollection<RepairService> repairServices = new Collection<RepairService>();
            foreach (var service in repairDto.services)
            {
                Service s = await _dbContext.Services.Where(s => s.Id == service.id).SingleOrDefaultAsync();
                var rs = new RepairService
                {
                    Service = s,
                    ServiceId = s.Id,
                    Repair = newRepair,
                    RepairId = newRepair.Id
                };
                repairServices.Add(rs);
                await _dbContext.RepairServices.AddAsync(rs);
            }
            newRepair.RepairServices = repairServices;
            await _dbContext.Repairs.AddAsync(newRepair);
            return newRepair;
        }
    }
}
