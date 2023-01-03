using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Data;
using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public class WorkshopRepositoryService : IWorkshopRepositoryService
    {
        private readonly WorkshopDbContext _dbContext;
        public WorkshopRepositoryService(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Workshop?> GetWorkshopAsync(string id)
            => await _dbContext.Workshops.Where(w => w.Id == id).Include(w => w.Localization).SingleOrDefaultAsync();

        public IEnumerable<Models.Workshop> GetWorkshopsAsync()
           => _dbContext.Workshops.Include(w => w.Localization);

        public async Task<Models.Workshop> CreateWorkshopAsync(WorkshopDto workshop, WorkshopUser user)
        {
            Localization localization = new()
            {
                Id = Guid.NewGuid().ToString(),
                Latitude = workshop.localization.latitude,
                Longitude = workshop.localization.longitude
            };
            await _dbContext.Localizations.AddAsync(localization);
            Models.Workshop newWorkshop = new Models.Workshop
            {
                Id = Guid.NewGuid().ToString(),
                Name = workshop.name,
                LocalizationId = localization.Id,
                Localization = localization,
                OwnerId = user.Id,
            };
            await _dbContext.Workshops.AddAsync(newWorkshop);
            return newWorkshop;
        }
    }
}
