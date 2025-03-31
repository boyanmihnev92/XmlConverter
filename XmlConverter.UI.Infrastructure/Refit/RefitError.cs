namespace XmlConverter.UI.Infrastructure.Refit
{
    public class RefitError
    {
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
    }

    public class RefitErrorDetails : RefitError
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
