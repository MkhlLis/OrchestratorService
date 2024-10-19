using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Handlers;

public class OrchestratorHandler : IOrchestratorHandler
{
    private readonly IStore _store;
    private readonly ILogger _logger;

    public OrchestratorHandler(IStore store, ILogger<OrchestratorHandler> logger)
    {
        _store = store;
        _logger = logger;
    }
    
    public async Task AddNewOrderAsync(Order order, CancellationToken cancellationToken)
    {
        await _store.AddNewOrderAsync(order, cancellationToken);
    }

    public async Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken)
    {
        return await _store.GetAllRequests(cancellationToken);
    }

    public async Task SendRequestsToMonitoring(CancellationToken cancellationToken)
    {
        Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        var bookingRequests = await _store.GetAllRequests(cancellationToken);
        foreach (var request in bookingRequests)
        {
            _logger.LogInformation($"Sending requests to the monitoring service \n"
                                   + $"the product {request.Title} is requested by customers with ids " +
                                   $"{string.Join(", ", request.Customers
                                       .Select(x => x.Id.ToString())
                                       .ToArray())}");   
        }
    }
}