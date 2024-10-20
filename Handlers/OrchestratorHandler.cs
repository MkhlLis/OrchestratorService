using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Handlers;

public class OrchestratorHandler : IOrchestratorHandler
{
    private readonly IStore _store;
    private readonly ILogger _logger;
    private readonly IProduceEventService _produceEventService;
    private readonly IEventMapper<BookingRequest, BookingRequestEvent> _eventMapper;

    public OrchestratorHandler(
        IStore store,
        ILogger<OrchestratorHandler> logger,
        IProduceEventService produceEventService,
        IEventMapper<BookingRequest, BookingRequestEvent> eventMapper)
    {
        _store = store;
        _logger = logger;
        _produceEventService = produceEventService;
        _eventMapper = eventMapper;
    }
    
    public async Task AddNewOrderAsync(OrderBase orderBase, CancellationToken cancellationToken)
    {
        await _store.AddNewOrderAsync(orderBase, cancellationToken);
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
        
        await _produceEventService.ProduceAsync(bookingRequests.Select(x => _eventMapper.Map(x)).ToList(), cancellationToken);
    }

    public async Task OrderEvent(OrderBase orderBase, CancellationToken cancellationToken)
    {
        await _store.OrderEvent(orderBase, cancellationToken);
        await SendRequestsToMonitoring(cancellationToken);
    }
}