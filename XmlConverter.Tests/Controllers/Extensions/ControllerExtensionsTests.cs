using Microsoft.AspNetCore.Mvc;
using XmlConverter.Api.Infrastructure;
using XmlConverter.Application.Common.Models;

namespace XmlConverter.Tests.Controllers.Extensions
{
    public class ControllerExtensionsTests
    {
        private class DummyController : ControllerBase { }

        [Fact]
        public void Respond_WithValidResult_ReturnsOkObjectResult()
        {
            // Arrange
            var controller = new DummyController();
            var data = new FileResponse { FileName = "test.json", ContentType = "application/json" };

            // Act
            var result = controller.Respond(data);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<FileResponse>>(okResult.Value);

            Assert.Equal(data.FileName, response.Data!.FileName);
            Assert.Equal(string.Empty, response.Errors);
        }

        [Fact]
        public void Respond_WithNullResult_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var controller = new DummyController();

            // Act
            var result = controller.Respond<FileResponse>(null!);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<FileResponse>>(badRequest.Value);

            Assert.Null(response.Data);
            Assert.Equal("Error occurred while processing request", response.Errors);
        }
    }
}
