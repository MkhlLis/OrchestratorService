using Orchestrator.Contracts.Models;

namespace Orchestrator.Contracts.Interfaces;

/// <summary>
/// Сервис отправки ивентов.
/// </summary>
public interface IProduceEventService
{
    Task ProduceAsync(IEnumerable<BookingRequestEvent> content, CancellationToken cancellationToken);
}