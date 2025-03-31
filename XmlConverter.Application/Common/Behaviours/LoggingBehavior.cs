using MediatR;
using Microsoft.Extensions.Logging;

namespace XmlConverter.Application.Common.Behaviours;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        this._logger.LogInformation("Request: {Name}", requestName);

        var response = await next();

        this._logger.LogInformation("Response: {Name}", requestName);

        return response;
    }
}
