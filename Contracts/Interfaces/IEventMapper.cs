namespace Orchestrator.Contracts.Interfaces;

/// <summary>
/// Маппер ивентов.
/// </summary>
public interface IEventMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
    TSource Map(TDestination destination);
}