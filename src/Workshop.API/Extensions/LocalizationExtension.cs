using Workshop.API.Dtos;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class LocalizationExtension
    {
        public static LocalizationDto AsDto(this Localization localization)
        {
            return new LocalizationDto(localization.Id, localization.Latitude, localization.Longitude);
        }
    }
}
