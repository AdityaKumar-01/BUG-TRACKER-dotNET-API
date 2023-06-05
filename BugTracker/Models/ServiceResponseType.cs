namespace BugTracker.Models.ServiceResponseType;

public class ServiceResponseType<T>
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public T Payload { get; set; }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Ok",
            201 => "Created",
            204 => "No Content",
            400 => "A bad request",
            401 => "Not Authorized",
            403 => "Forbbiden action",
            404 => "Resource not found ",
            500 => "Internal Servor Error",
            502 => "Bad GateWay",
            _ => null
        };
    }
    public ServiceResponseType(int statusCode, T payload)
    {
        StatusCode = statusCode;
        StatusMessage = GetDefaultMessageForStatusCode(statusCode);
        Payload = payload;
    }
    public ServiceResponseType(int statusCode)
    {
        StatusCode = statusCode;
        StatusMessage = GetDefaultMessageForStatusCode(statusCode);
        Payload = default(T);
    }
}
