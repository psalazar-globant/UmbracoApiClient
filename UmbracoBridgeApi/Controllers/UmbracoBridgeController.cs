using Microsoft.AspNetCore.Mvc;
using UmbracoBridgeApi.DataTransferObjects;
using UmbracoBridgeApi.Services;

namespace UmbracoBridgeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UmbracoBridgeController : ControllerBase
{
    private readonly IDocumentTypeService _documentService;

    public UmbracoBridgeController(IDocumentTypeService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("Healthcheck")]
    public async Task<IActionResult> Healthcheck()
    {
        var result = await _documentService.HealthCheckAsync();

        return result.Match(data => Ok(data), error => throw error!);
    }


    [HttpPost("DocumentType")]
    public async Task<IActionResult> CreateDocumentType([FromBody] DocumentTypeRequestDto request)
    {

        var result = await _documentService.CreateDocumentTypeAsync(request);

        return result.Match(data => Ok(data), error => throw error!);
    }

    [HttpDelete("DocumentType/{id}")]
    public async Task<IActionResult> DeleteDocumentType(Guid id)
    {
        var result = await _documentService.DeleteDocumentTypeAsync(id);

        return result.Match(data => Ok(data), error => throw error!);
    }
}