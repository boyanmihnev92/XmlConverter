using MediatR;
using Microsoft.AspNetCore.Mvc;
using XmlConverter.Api.Infrastructure;
using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Common.Models;
using XmlConverter.Application.Features.FileConvertion;

namespace XmlConverter.Api.Controllers;

[ApiController]
[Route("/convert-xml")]
public class ConvertXmlController(ISender sender) : ControllerBase
{
    [HttpPost("to-json")]
    [ProducesResponseType(typeof(ApiResponse<FileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<FileResponse>), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> ConvertToJson([FromBody] FileUploadRequest request, CancellationToken cancellationToken)
    {
        var command = new XmlConvertCommand(request.FileData, request.FileName, ConvertTo.Json);
        var result = await sender.Send(command);

        return this.Respond(result);
    }
}
