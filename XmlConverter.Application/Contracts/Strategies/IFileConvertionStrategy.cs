using System.Xml.Linq;
using XmlConverter.Application.Common.Models;

namespace XmlConverter.Application.Contracts.Strategies
{
    public interface IFileConvertionStrategy
    {
        FileResponse ConvertFile(XDocument xmlDocument, string fileName);
    }
}
