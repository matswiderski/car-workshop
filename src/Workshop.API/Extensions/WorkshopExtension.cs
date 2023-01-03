using Workshop.API.Dtos;

namespace Workshop.API.Extensions
{
    public static class WorkshopExtension
    {
        public static WorkshopDto AsDto(this Models.Workshop workshop)
        {
            return new WorkshopDto(workshop.Id, workshop.Name, workshop.Localization.AsDto());
        }
    }
}
