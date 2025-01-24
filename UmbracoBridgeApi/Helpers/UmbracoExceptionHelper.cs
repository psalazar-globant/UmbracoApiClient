using System.Net;
using UmbracoBridgeApi.Exceptions;

namespace UmbracoBridgeApi.Helpers;

public static class UmbracoExceptionHelper
{
    public static async Task<UmbracoException> CreateUmbracoException(HttpResponseMessage response)
    {
        var errorMessage = await response.Content.ReadAsStringAsync();
        return response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => new UmbracoException(response.StatusCode, $"Authenticate to have access to api: {errorMessage}"),
            HttpStatusCode.InternalServerError => new UmbracoException(response.StatusCode, $"An internal server error has occurred: {errorMessage}"),
            HttpStatusCode.NotFound => new UmbracoException(response.StatusCode, $"Resource could not be found: {errorMessage}"),
            HttpStatusCode.BadRequest => new UmbracoException(response.StatusCode, $"Bad request: {errorMessage}"),
            _ => new UmbracoException(response.StatusCode, $"An internal error thrown. See details: {errorMessage}"),
        };
    }
}
