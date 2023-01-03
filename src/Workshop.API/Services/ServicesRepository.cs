using Microsoft.AspNetCore.Identity;
using System.Collections;
using Workshop.API.Data;
using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly WorkshopDbContext _dbContext;
        public ServicesRepository(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Service> GetServices()
            => _dbContext.Services;
    }
}
