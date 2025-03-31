using System.Text;
using System.Xml.Linq;
using Moq;
using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Common.Exceptions;
using XmlConverter.Application.Common.Models;
using XmlConverter.Application.Contracts.Factories;
using XmlConverter.Application.Contracts.Strategies;
using XmlConverter.Application.Features.FileConvertion;

namespace XmlConverter.Tests.Features
{
    public class XmlConvertCommandTests
    {
        private readonly Mock<IFileConvertionFactory> _factoryMock;

        public XmlConvertCommandTests()
        {
            _factoryMock = new Mock<IFileConvertionFactory>();
        }

        [Fact]
        public async Task Handle_ValidXmlAndSupportedConversion_ReturnsFileResponse()
        {
            // Arrange
            var validXml = "<root><name>test</name></root>";
            var fileData = Encoding.UTF8.GetBytes(validXml);
            var fileName = "test.xml";

            var expectedResponse = new FileResponse
            {
                FileName = "converted.json",
                FileContent = Encoding.UTF8.GetBytes("{}"),
                ContentType = "application/json"
            };

            var strategyMock = new Mock<IFileConvertionStrategy>();
            strategyMock.Setup(s => s.ConvertFile(It.IsAny<XDocument>(), fileName)).Returns(expectedResponse);

            _factoryMock.Setup(f => f.GetStrategy(ConvertTo.Json)).Returns(strategyMock.Object);

            var handler = new XmlConvertCommand.ConvertFileCommandHandler(_factoryMock.Object);
            var command = new XmlConvertCommand(fileData, fileName, ConvertTo.Json);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.FileName, result.FileName);
            Assert.Equal(expectedResponse.ContentType, result.ContentType);
        }

        [Theory]
        [InlineData("invalid xml")]
        [InlineData("<?xml><invalid>")]
        public async Task Handle_InvalidXml_ThrowsCustomValidationException(string invalidXml)
        {
            // Arrange
            var fileData = Encoding.UTF8.GetBytes(invalidXml);
            var fileName = "bad.xml";
            var command = new XmlConvertCommand(fileData, fileName, ConvertTo.Json);

            var handler = new XmlConvertCommand.ConvertFileCommandHandler(_factoryMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<CustomValidationException>(() => handler.Handle(command, default));
            Assert.Contains("FileData", ex.Errors.Keys);
        }

        [Fact]
        public async Task Handle_UnsupportedConversionType_ThrowsValidationException()
        {
            // Arrange
            var validXml = "<root><item>123</item></root>";
            var fileData = Encoding.UTF8.GetBytes(validXml);
            var fileName = "test.xml";

            _factoryMock.Setup(f => f.GetStrategy(It.IsAny<ConvertTo>())).Returns<IFileConvertionStrategy>(null!);

            var handler = new XmlConvertCommand.ConvertFileCommandHandler(_factoryMock.Object);
            var command = new XmlConvertCommand(fileData, fileName, (ConvertTo)999); // unsupported enum value

            // Act & Assert
            var ex = await Assert.ThrowsAsync<CustomValidationException>(() => handler.Handle(command, default));
            Assert.Contains("ConverTo", ex.Errors.Keys);
        }
    }
}
