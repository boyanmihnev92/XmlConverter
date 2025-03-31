using System.Xml.Linq;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Common.Exceptions;
using XmlConverter.Application.Common.Models;
using XmlConverter.Application.Contracts.Factories;

namespace XmlConverter.Application.Features.FileConvertion;

public sealed record XmlConvertCommand(
    byte[] FileData,
    string FileName,
    ConvertTo ConvertTo) : IRequest<FileResponse>
{
    public sealed class ConvertFileCommandHandler(IFileConvertionFactory factory) : IRequestHandler<XmlConvertCommand, FileResponse>
    {
        public async Task<FileResponse> Handle(XmlConvertCommand request, CancellationToken cancellationToken)
        {
            XDocument xmlDoc;
            try
            {
                using var stream = new MemoryStream(request.FileData);
                xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, cancellationToken) ?? new();
            }
            catch (Exception ex)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("FileData", ex.Message)
                };

                throw new CustomValidationException(failures);
            }

            var strategy = factory.GetStrategy(request.ConvertTo);
            if (strategy == null)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("ConverTo", "Currently we do not support this type of conversion")
                };

                throw new CustomValidationException(failures);
            }

            var result = strategy.ConvertFile(xmlDoc, request.FileName);

            return await Task.FromResult(result);
        }
    }

    public sealed class XmlConvertCommandValidator : AbstractValidator<XmlConvertCommand>
    {
        public XmlConvertCommandValidator()
        {
            RuleFor(v => v.FileData).NotNull().WithMessage("XML document cannot be empty!");
            RuleFor(v => v.FileName).NotNull().WithMessage("Uploaded file must have a name!");
        }
    }
}
