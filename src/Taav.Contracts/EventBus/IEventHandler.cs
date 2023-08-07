using Taav.Contracts.Interfaces;

namespace Taav.Contracts.EventBus;

public interface IEventHandler<in TEvent>
    where TEvent : class, IEvent
{
    Task HandleEventAsync(TEvent @event);
}
