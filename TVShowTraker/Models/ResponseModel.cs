namespace TVShowTraker.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Status { get; internal set; }
    }
}
