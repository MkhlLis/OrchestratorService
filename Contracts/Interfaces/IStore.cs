using Orchestrator.Contracts.Models;

namespace Orchestrator.Contracts.Interfaces;

/// <summary>
/// Интерфейс хранилища.
/// </summary>
public interface IStore
{
    Task AddNewOrderAsync(Order order, CancellationToken cancellationToken);
    
    Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken);
}