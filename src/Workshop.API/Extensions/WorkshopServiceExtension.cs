using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class WorkshopServiceExtension
    {
        public static ServiceDto AsDto(this Service service)
        {
            return new ServiceDto(service.Id, service.Name, service.Price, service.Category);
        }
    }
}
