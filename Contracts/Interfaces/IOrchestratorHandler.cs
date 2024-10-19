using Orchestrator.Contracts.Models;

namespace Orchestrator.Contracts.Interfaces;

public interface IOrchestratorHandler
{
    Task AddNewOrderAsync(Order order, CancellationToken cancellationToken);

    Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken);

    Task SendRequestsToMonitoring(CancellationToken cancellationToken);
}