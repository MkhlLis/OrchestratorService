using Microsoft.AspNetCore.Mvc;
using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Controllers;

/// <summary>
/// Контроллер оркестратора.
/// </summary>
[ApiController]
[Route("orchestrator")]
public class OrchestratorController
{
    private readonly IOrchestratorHandler _handler;
    
    public OrchestratorController(IOrchestratorHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("add-new-order")]
    public async Task AddNewOrder(Order order, CancellationToken cancellationToken)
    {
        await _handler.AddNewOrderAsync(order, cancellationToken);
    }

    [HttpGet("get-all-orders")]
    public async Task<IEnumerable<BookingRequest>> GetOrders(CancellationToken cancellationToken)
    {
        return await _handler.GetAllRequests(cancellationToken);
    }

    [HttpGet("request-to-monitoring")]
    public async Task SendRequestsToMonitoring(CancellationToken cancellationToken)
    {
        await _handler.SendRequestsToMonitoring(cancellationToken);
    }
}