using FluentValidation.Results;

namespace XmlConverter.Application.Common.Exceptions
{
    public class CustomValidationException() : Exception("One or more validation failures have occurred!")
    {
        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            this.Errors = failures
                .GroupBy(v => v.PropertyName, v => v.ErrorMessage)
                .ToDictionary(v => v.Key, v => v.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
            = new Dictionary<string, string[]>();
    }
}
