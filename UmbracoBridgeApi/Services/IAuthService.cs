using System.Net;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using UmbracoBridgeApi.Configuration;

namespace UmbracoBridgeApi.Services;

public interface IAuthService
{
    string GetAccessToken();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly UmbracoBridgeSettings _umbracoSettings;
    private string? _accessToken;
    private DateTime _accessTokenExpiry = DateTime.MinValue;
    private readonly object _lock = new();

    public AuthService(HttpClient httpClient, IOptions<UmbracoBridgeSettings> umbracoSettings)
    {
        _httpClient = httpClient;
        _umbracoSettings = umbracoSettings.Value;
    }

    public string GetAccessToken()
    {
        if (_accessTokenExpiry > DateTime.UtcNow)
        {
            return _accessToken!;
        }

        lock (_lock)
        {
            if (_accessTokenExpiry > DateTime.UtcNow)
            {
                return _accessToken!;
            }

            var tokenResponse = _httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _umbracoSettings.TokenEndpoint,
                    ClientId = _umbracoSettings.ClientId,
                    ClientSecret = _umbracoSettings.ClientSecret
                }).GetAwaiter().GetResult();

            if (tokenResponse.IsError || tokenResponse.AccessToken is null)
            {
                throw new Exception($"Error obtaining client token. {tokenResponse.ErrorDescription}");
            }

            _accessToken = tokenResponse.AccessToken;
            _accessTokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 20);
        }

        return _accessToken!;
    }
}
