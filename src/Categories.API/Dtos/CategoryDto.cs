namespace Categories.API.Dtos
{
    public record CategoryDto(string Name, string Description, ICollection<string> Parts);
}
