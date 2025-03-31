using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using XmlConverter.Api.Controllers;
using XmlConverter.Application.Common.Models;
using XmlConverter.Application.Features.FileConvertion;

namespace XmlConverter.Tests.Controllers
{
    public class ConvertXmlControllerTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly ConvertXmlController _controller;

        public ConvertXmlControllerTests()
        {
            _senderMock = new Mock<ISender>();
            _controller = new ConvertXmlController(_senderMock.Object);
        }

        [Fact]
        public async Task ConvertToJson_WithValidRequest_ReturnsOkWithFileResponse()
        {
            // Arrange
            var fileData = Encoding.UTF8.GetBytes("<root><name>Test</name></root>");
            var request = new FileUploadRequest(fileData, "test.xml");

            var expectedResponse = new FileResponse
            {
                FileName = "test.json",
                ContentType = "application/json",
                FileContent = Encoding.UTF8.GetBytes("{ \"name\": \"Test\" }")
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<XmlConvertCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ConvertToJson(request, default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<FileResponse>>(okResult.Value);
            Assert.Equal(expectedResponse.FileName, apiResponse.Data!.FileName);
            Assert.Equal(expectedResponse.ContentType, apiResponse.Data.ContentType);
            Assert.Equal(string.Empty, apiResponse.Errors);
        }

        [Fact]
        public async Task ConvertToJson_WithNullResult_ReturnsBadRequest()
        {
            // Arrange
            var request = new FileUploadRequest([], "test.xml");

            _senderMock
                .Setup(s => s.Send(It.IsAny<XmlConvertCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((FileResponse?)null);

            // Act
            var result = await _controller.ConvertToJson(request, default);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<FileResponse>>(badRequest.Value);

            Assert.Null(apiResponse.Data);
            Assert.False(string.IsNullOrWhiteSpace(apiResponse.Errors));
        }
    }
}
