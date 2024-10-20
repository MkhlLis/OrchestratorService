using Orchestrator.Contracts.Models;

namespace Orchestrator.Contracts.Interfaces;

public interface IOrchestratorHandler
{
    Task AddNewOrderAsync(OrderBase orderBase, CancellationToken cancellationToken);

    Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken);

    Task SendRequestsToMonitoring(CancellationToken cancellationToken);

    Task OrderEvent(OrderBase orderBase, CancellationToken cancellationToken);
}