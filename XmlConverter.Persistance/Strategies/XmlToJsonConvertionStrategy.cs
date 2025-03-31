using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using XmlConverter.Application.Common.Models;
using XmlConverter.Application.Contracts.Strategies;

namespace XmlConverter.Persistance.Strategies
{
    public sealed class XmlToJsonConvertionStrategy : IXmlToJsonConvertionStrategy
    {
        public FileResponse ConvertFile(XDocument xmlDocument, string fileName)
        {
            var json = JsonConvert.SerializeXNode(xmlDocument, Newtonsoft.Json.Formatting.Indented);
            var fileData = Encoding.UTF8.GetBytes(json);

            return new FileResponse()
            {
                FileContent = fileData,
                ContentType = "application/json",
                FileName = Path.GetFileNameWithoutExtension(fileName),
            };
        }
    }
}
