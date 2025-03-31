using FluentValidation;
using MediatR;
using XmlConverter.Application.Common.Exceptions;

namespace XmlConverter.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .ToList();

            var failuresCount = failures?.Count ?? 0;

            if (failuresCount > 0)
            {
                throw new CustomValidationException(failures!);
            }

            return await next();
        }
    }
}
