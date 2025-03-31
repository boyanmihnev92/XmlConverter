using FluentValidation;

namespace XmlConverter.Application.Common.Models
{
    public sealed record FileUploadRequest(byte[] FileData, string FileName);

    public sealed class FileUploadRequestValidator : AbstractValidator<FileUploadRequest>
    {
        public FileUploadRequestValidator()
        {
            RuleFor(v => v.FileData).NotNull().WithMessage("Invalid file! File must have content");
            RuleFor(v => v.FileName).NotNull().WithMessage("Invalid file! File must have name");
        }
    }
}
