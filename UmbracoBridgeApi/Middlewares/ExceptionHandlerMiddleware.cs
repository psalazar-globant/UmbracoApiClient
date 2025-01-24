using System;
using System.Net;
using Newtonsoft.Json;
using UmbracoBridgeApi.Exceptions;

namespace UmbracoBridgeApi.Middlewares;

public class ExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    public ExceptionHandlerMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        switch (exception)
        {
            case ResourceNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UmbracoException umbracoException:
                code = umbracoException.StatusCode;
                break;
            case Exception:
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonConvert.SerializeObject(exception.Message));
    }
}
