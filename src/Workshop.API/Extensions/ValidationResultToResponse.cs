using FluentValidation.Results;

namespace Workshop.API.Extensions
{
    public static class ValidationResultToResponse
    {
        private const string _title = "One or more validation errors occurred.";
        public static object ToResponseObject(this ValidationResult result, int status)
        {
            Dictionary<string, string[]> errors = new();
            var distinctNames = result.Errors.DistinctBy(x => x.PropertyName).Select(x => x.PropertyName);
            foreach (var property in distinctNames)
                errors.Add(property, result.Errors.Where(e => e.PropertyName == property).Select(e => e.ErrorMessage).ToArray());
            return new
            {
                title = _title,
                status,
                Errors = errors
            };
        }
    }
}
