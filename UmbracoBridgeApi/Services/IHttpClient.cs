
using IdentityModel.Client;

namespace UmbracoBridgeApi.Services;
public interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string requestUri);
    Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value);
    Task<HttpResponseMessage> DeleteAsync(string requestUri);
    void SetBearerToken(string token);
}
public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await _httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        return await _httpClient.PostAsJsonAsync(requestUri, value);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await _httpClient.DeleteAsync(requestUri);
    }

    public void SetBearerToken(string token)
    {
        _httpClient.SetBearerToken(token);
    }
}