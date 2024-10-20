using Orchestrator.Contracts.Models;

namespace Orchestrator.Contracts.Interfaces;

/// <summary>
/// Интерфейс хранилища.
/// </summary>
public interface IStore
{
    Task AddNewOrderAsync(OrderBase orderBase, CancellationToken cancellationToken);
    
    Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken);
    
    Task OrderEvent(OrderBase orderBase, CancellationToken cancellationToken);
}