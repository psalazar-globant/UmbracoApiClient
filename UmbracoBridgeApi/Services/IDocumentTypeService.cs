using IdentityModel.Client;
using Microsoft.Extensions.Options;
using UmbracoBridgeApi.Configuration;
using UmbracoBridgeApi.DataTransferObjects;
using UmbracoBridgeApi.Exceptions;
using UmbracoBridgeApi.Helpers;
using UmbracoBridgeApi.Models;

namespace UmbracoBridgeApi.Services;

public interface IDocumentTypeService
{
    Task<Result<CreateDocumentTypeResponseDto>> CreateDocumentTypeAsync(DocumentTypeRequestDto request);
    Task<Result<string>> DeleteDocumentTypeAsync(Guid id);
    Task<Result<string>> HealthCheckAsync();

}

public class DocumentTypeService : IDocumentTypeService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IAuthService _authService;
    private readonly UmbracoBridgeSettings _umbracoSettings;

    public DocumentTypeService(IHttpClientService httpClient, IAuthService authService, IOptions<UmbracoBridgeSettings> umbracoSettings)
    {
        _httpClientService = httpClient;
        _authService = authService;
        _umbracoSettings = umbracoSettings.Value;
    }

    public async Task<Result<CreateDocumentTypeResponseDto>> CreateDocumentTypeAsync(DocumentTypeRequestDto request)
    {
        SetBearerToken();

        var response = await _httpClientService.PostAsJsonAsync($"{_umbracoSettings.UmbracoBaseUrl}{_umbracoSettings.ApiEndpoints.PostDocumentTypeUrl}", request);

        return await HandleDocumentTypeResponse(response);
    }

    public async Task<Result<string>> DeleteDocumentTypeAsync(Guid id)
    {
        SetBearerToken();

        var response = await _httpClientService.DeleteAsync($"{_umbracoSettings.UmbracoBaseUrl}{_umbracoSettings.ApiEndpoints.DeleteDocumentUrl}{id}");

        return await HandleDeleteDocumentTypeResponse(response,id);
    }


    public async Task<Result<string>> HealthCheckAsync()
    {
        SetBearerToken();

        var response = await _httpClientService.GetAsync($"{_umbracoSettings.UmbracoBaseUrl}{_umbracoSettings.ApiEndpoints.HealthCheckUrl}");

        return await HandleResponse(response);
    }

    private void SetBearerToken()
    {
        var accessToken = _authService.GetAccessToken();
        _httpClientService.SetBearerToken(accessToken);
    }

    private async Task<Result<CreateDocumentTypeResponseDto>> HandleDocumentTypeResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await UmbracoExceptionHelper.CreateUmbracoException(response);
        }

        var resourceIdentifierResult = GetHeaderValue(response, "Umb-Generated-Resource");
        var locationResult = GetHeaderValue(response, "Location");

        if (!resourceIdentifierResult.IsSuccess)
            return resourceIdentifierResult.Error;
        if (!locationResult.IsSuccess)
            return locationResult.Error;

        return new CreateDocumentTypeResponseDto()
        {
            ResourceIdentifier = resourceIdentifierResult.Data,
            Location = locationResult.Data
        };
        
    }

    private async Task<Result<string>> HandleDeleteDocumentTypeResponse(HttpResponseMessage response, Guid documentGuid)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await UmbracoExceptionHelper.CreateUmbracoException(response);
        }

        return $"Document {documentGuid} succesfully deleted.";
    }

    private async Task<Result<string>> HandleResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await UmbracoExceptionHelper.CreateUmbracoException(response);
        }

        var content = await response.Content.ReadAsStringAsync();
        return content; 
    }

    private Result<string?> GetHeaderValue(HttpResponseMessage response, string headerName)
    {
        return response.Headers.TryGetValues(headerName, out var values) ? values.FirstOrDefault() : new ResourceNotFoundException("Value not found");
    }

}
