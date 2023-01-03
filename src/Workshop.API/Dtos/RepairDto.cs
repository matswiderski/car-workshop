namespace Workshop.API.Dtos
{
    public record RepairDto(string? id, CarDto car, WorkshopDto workshop, IEnumerable<ServiceDto> services, string message);
}
