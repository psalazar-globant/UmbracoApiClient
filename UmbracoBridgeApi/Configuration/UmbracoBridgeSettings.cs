namespace UmbracoBridgeApi.Configuration;

public class UmbracoBridgeSettings
{
    public required string UmbracoBaseUrl { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string TokenEndpoint { get; set; }

    public required ApiEndpoints ApiEndpoints { get; set; }
}
