using Microsoft.Extensions.DependencyInjection;
using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Contracts.Factories;
using XmlConverter.Application.Contracts.Strategies;

namespace XmlConverter.Persistance.Factories
{
    public sealed class FileConvertionFactory(IServiceProvider serviceProvider) : IFileConvertionFactory
    {
        public IFileConvertionStrategy GetStrategy(ConvertTo converTo) => converTo switch
        {
            ConvertTo.Json => serviceProvider.GetRequiredService<IXmlToJsonConvertionStrategy>(),
            ConvertTo.Unset => throw new NotSupportedException($"Conversion for {converTo} is not supported."),
            _ => throw new NotSupportedException($"Conversion for {converTo} is not supported."),
        };
    }
}
