namespace UmbracoBridgeApi.Configuration;

public class ApiEndpoints
{
    public required string HealthCheckUrl { get; set; }
    public required string PostDocumentTypeUrl { get; set; }
    public required string DeleteDocumentUrl { get; set; }
}
