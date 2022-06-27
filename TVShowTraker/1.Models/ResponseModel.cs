namespace TVShowTraker.Models
{
    public class ResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public string Status { get; internal set; } = string.Empty;

        public ResponseModel() { }
        public ResponseModel(string message, string status)
        {
            Message = message;
            Status = status;
        }
    }
}
