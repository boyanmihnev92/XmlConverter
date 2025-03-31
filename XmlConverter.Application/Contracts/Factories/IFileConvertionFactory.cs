using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Contracts.Strategies;

namespace XmlConverter.Application.Contracts.Factories
{
    public interface IFileConvertionFactory
    {
        IFileConvertionStrategy GetStrategy(ConvertTo fileType);
    }
}
