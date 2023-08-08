namespace Taav.Contracts.EventBus;

public interface IEventPublisher
{
    Task PublishEventAsync<TEvent>(TEvent @event)
        where TEvent : class, IEvent;
}
