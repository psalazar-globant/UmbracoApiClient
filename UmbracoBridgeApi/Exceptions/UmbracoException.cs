using System.Net;

namespace UmbracoBridgeApi.Exceptions;

public class UmbracoException: Exception
{
    public HttpStatusCode StatusCode { get; init; }

    public UmbracoException(HttpStatusCode statusCode, string message): base(message)
    {
        StatusCode = statusCode;   
    }
}
