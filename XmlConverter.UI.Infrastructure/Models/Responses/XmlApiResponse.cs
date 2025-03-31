namespace XmlConverter.UI.Infrastructure.Models.Responses
{
    public sealed class XmlApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Errors { get; set; }
        public bool? Success { get; set; }
    }
}
