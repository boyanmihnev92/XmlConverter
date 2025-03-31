using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using XmlConverter.UI.Infrastructure.Enums;

namespace XmlConverter.UI.Components
{
    public static class FileHelper
    {
        public static async Task DownloadFile(byte[] fileData, string fileName, string mimeType, IJSRuntime jsRuntime)
        {
            var fileStream = new MemoryStream(fileData);
            using var streamRef = new DotNetStreamReference(stream: fileStream);

            await jsRuntime!.InvokeVoidAsync("downloadFileFromStream", fileName, mimeType, streamRef);
        }

        public static async Task<byte[]> GetFileData(IBrowserFile file, AllowedFileSizes allowedFileSize)
        {
            var stream = file.OpenReadStream(maxAllowedSize: (long)allowedFileSize);
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();

            return ms.ToArray();
        }

        public static Stream GetFileStream(IBrowserFile file, AllowedFileSizes allowedFileSize)
        {
            var stream = file.OpenReadStream(maxAllowedSize: (long)allowedFileSize);
            return stream;
        }
    }
}
