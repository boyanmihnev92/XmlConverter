using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using XmlConverter.Persistance.Strategies;

namespace XmlConverter.Tests.Strategies
{
    public class XmlToJsonConvertionStrategyTests
    {
        private readonly XmlToJsonConvertionStrategy _strategy;

        public XmlToJsonConvertionStrategyTests()
        {
            _strategy = new XmlToJsonConvertionStrategy();
        }

        [Fact]
        public void ConvertFile_WithValidXml_ReturnsCorrectJsonFileResponse()
        {
            // Arrange
            var xml = XDocument.Parse("<root><name>Test</name></root>");
            var fileName = "sample.xml";

            // Act
            var result = _strategy.ConvertFile(xml, fileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("application/json", result.ContentType);
            Assert.Equal("sample", result.FileName);

            var expectedJson = JsonConvert.SerializeXNode(xml, Formatting.Indented);
            var actualJson = Encoding.UTF8.GetString(result.FileContent!);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void ConvertFile_WithNestedXml_ProducesCorrectJsonStructure()
        {
            // Arrange
            var xml = XDocument.Parse(@"
            <root>
                <person>
                    <name>John</name>
                    <age>30</age>
                </person>
            </root>");

            var result = _strategy.ConvertFile(xml, "person.xml");

            // Act
            var json = Encoding.UTF8.GetString(result.FileContent!);

            // Assert
            Assert.Contains("\"person\"", json);
            Assert.Contains("\"name\": \"John\"", json);
            Assert.Contains("\"age\": \"30\"", json);
        }

        [Fact]
        public void ConvertFile_WithEmptyXml_ReturnsEmptyJson()
        {
            // Arrange
            var xml = new XDocument(new XElement("root"));
            var result = _strategy.ConvertFile(xml, "empty.xml");

            // Act
            var json = Encoding.UTF8.GetString(result.FileContent!);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("\"root\"", json);
            Assert.Equal("empty", result.FileName);
        }
    }
}
