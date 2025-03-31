using Moq;
using XmlConverter.Application.Common.Enums;
using XmlConverter.Application.Contracts.Strategies;
using XmlConverter.Persistance.Factories;

namespace XmlConverter.Tests.Factories
{
    public class FileConvertionFactoryTests
    {
        [Fact]
        public void GetStrategy_WithJson_ReturnsIXmlToJsonConvertionStrategy()
        {
            // Arrange
            var strategyMock = new Mock<IXmlToJsonConvertionStrategy>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IXmlToJsonConvertionStrategy)))
                .Returns(strategyMock.Object);

            var factory = new FileConvertionFactory(serviceProviderMock.Object);

            // Act
            var result = factory.GetStrategy(ConvertTo.Json);

            // Assert
            Assert.NotNull(result);
            Assert.Same(strategyMock.Object, result);
        }

        [Theory]
        [InlineData(ConvertTo.Unset)]
        [InlineData((ConvertTo)999)]
        public void GetStrategy_WithUnsupportedType_ThrowsNotSupportedException(ConvertTo convertTo)
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var factory = new FileConvertionFactory(serviceProviderMock.Object);

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => factory.GetStrategy(convertTo));
            Assert.Contains("Conversion for", ex.Message);
        }
    }
}
