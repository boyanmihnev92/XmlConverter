namespace XmlConverter.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Errors { get; set; }
        public bool? Success
        {
            get
            {
                return string.IsNullOrEmpty(Errors);
            }
        }
    }
}
