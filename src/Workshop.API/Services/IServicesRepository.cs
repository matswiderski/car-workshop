using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface IServicesRepository
    {
        IEnumerable<Service> GetServices();
    }
}