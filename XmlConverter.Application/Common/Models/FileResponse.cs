namespace XmlConverter.Application.Common.Models
{
    public sealed class FileResponse
    {
        public byte[]? FileContent { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
