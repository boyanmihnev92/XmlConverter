using Refit;
using XmlConverter.UI.Components.ModalWindowService;
using XmlConverter.UI.Infrastructure.Models.Responses;
using XmlConverter.UI.Infrastructure.Refit;

namespace XmlConverter.UI.Extensions
{
    public static class ApiResponseExtensions
    {
        public static async Task<bool> IsValid(
            this ApiResponse<XmlApiResponse<FileResponse>> response,
            IModalWindowService modalService)
        {
            var error = response.Error?.Content;
            if (!string.IsNullOrWhiteSpace(error))
            {
                var errorMessage = error.ToUserMessage();
                await modalService.Alert(errorMessage);
                return false;
            }

            if (!response.IsSuccessStatusCode || response?.Content == null)
            {
                await modalService.Alert("Something went wrong while processing file!");
                return false;
            }

            if (response.Content?.Data?.FileContent?.Length == 0)
            {
                await modalService.Alert("Something went wrong while generating JSON file!");
                return false;
            }

            return true;
        }
    }
}
