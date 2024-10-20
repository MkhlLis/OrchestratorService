using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task AddNewOrder(OrderBase orderBase, CancellationToken cancellationToken)
    {
        await _handler.AddNewOrderAsync(orderBase, cancellationToken);
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

    [HttpPost("order-event")]
    public async Task OrderEvent([FromBody] OrderBase orderBase, CancellationToken cancellationToken)
    {
        await _handler.OrderEvent(orderBase, cancellationToken);
    }
}