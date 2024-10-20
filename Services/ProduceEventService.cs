using System.Text;
using System.Text.Json;
using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Services;

public class ProduceEventService : IProduceEventService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProduceEventService> _logger;

    public ProduceEventService(HttpClient httpClient, ILogger<ProduceEventService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task ProduceAsync(IEnumerable<BookingRequestEvent> content, CancellationToken cancellationToken)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");
        using var response = await _httpClient.PostAsync("http://localhost:5039/administration/set-queue", jsonContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError((int)response.StatusCode, "Failed to produce event");
        }
    }
}