namespace XmlConverter.UI.Infrastructure.Models.Responses;

public sealed class FileResponse
{
    public byte[]? FileContent { get; set; }

    public string? ContentType { get; set; }

    public string? FileName { get; set; }
}
