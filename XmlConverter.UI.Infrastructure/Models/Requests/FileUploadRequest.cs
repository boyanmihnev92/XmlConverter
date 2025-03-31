namespace XmlConverter.UI.Infrastructure.Models.Requests;

public sealed record FileUploadRequest(byte[] FileData, string FileName);
