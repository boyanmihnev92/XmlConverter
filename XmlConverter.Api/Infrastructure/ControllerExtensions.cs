using Microsoft.AspNetCore.Mvc;
using XmlConverter.Application.Common.Models;

namespace XmlConverter.Api.Infrastructure;

public static class ControllerExtensions
{
    public static IActionResult Respond<T>(this ControllerBase controllerBase, T result)
    {
        var response = new ApiResponse<T>
        {
            Data = result,
            Errors = result == null ? "Error occurred while processing request" : string.Empty
        };

        return result == null
            ? controllerBase.BadRequest(response)
            : controllerBase.Ok(response);
    }
}
