using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface IWorkshopRepositoryService
    {
        Task<Models.Workshop> CreateWorkshopAsync(WorkshopDto workshop, WorkshopUser user);
        Task<Models.Workshop?> GetWorkshopAsync(string id);
        IEnumerable<Models.Workshop> GetWorkshopsAsync();
    }
}