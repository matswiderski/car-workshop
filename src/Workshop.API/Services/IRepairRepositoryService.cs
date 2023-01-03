using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface IRepairRepositoryService
    {
        Task<Repair> CreateRepairAsync(RepairDto repairDto, WorkshopUser user);
        Task<Repair?> GetRepairAsync(string id);
        IEnumerable<Repair> GetUserRepairs(string userId);
    }
}