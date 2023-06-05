namespace BugTracker.Models
{
    public class ServiceResponseType<T>
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Payload { get; set; }

        ServiceResponseType(string statusCode, string statusMessage, T payload)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
            Payload = payload;
        }
    }
}
