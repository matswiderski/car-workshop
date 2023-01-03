using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class RepairExtension
    {
        public static RepairDto AsDto(this Repair repair)
        {
            return new RepairDto(repair.Id, repair.Car.AsDto(), repair.Workshop.AsDto(), repair.RepairServices.Select(r => r.Service.AsDto()), repair.Message);
        }
    }
}
