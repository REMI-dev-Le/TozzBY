namespace API.Error
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            this.message = message;
            Details = details;
        }

        public int StatusCode { get; set; }

        public string message { get; set; }

        public string Details { get; set; }
    }
}