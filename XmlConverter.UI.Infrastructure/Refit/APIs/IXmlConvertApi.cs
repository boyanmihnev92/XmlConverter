using Refit;
using XmlConverter.UI.Infrastructure.Models.Requests;
using XmlConverter.UI.Infrastructure.Models.Responses;

namespace XmlConverter.UI.Infrastructure.Refit.APIs
{
    public interface IXmlConvertApi
    {
        [Post("/convert-xml/to-json")]
        Task<ApiResponse<XmlApiResponse<FileResponse>>> ConvertXmlToJson([Body] FileUploadRequest request);
    }
}
