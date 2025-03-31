using System.Text.Json;
using Refit;

namespace XmlConverter.UI.Infrastructure.Refit
{
    public static class RefitErrorExtensions
    {
        public static string ToUserMessage(this string? errorContent)
        {
            if (string.IsNullOrWhiteSpace(errorContent))
                return "An unknown error occurred.";

            try
            {
                var validation = JsonSerializer.Deserialize<RefitErrorDetails>(
                    errorContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (validation?.Errors?.Any() == true)
                {
                    var allErrors = validation.Errors
                        .SelectMany(v => v.Value)
                        .ToList();

                    return string.Join("\n", allErrors);
                }

                var problem = JsonSerializer.Deserialize<ProblemDetails>(
                    errorContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return problem?.Detail ?? problem?.Title ?? "An error occurred.";
            }
            catch
            {
                return "An error occurred.";
            }
        }
    }
}
