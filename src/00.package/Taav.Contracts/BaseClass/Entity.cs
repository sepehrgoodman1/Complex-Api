
using Taav.Contracts.Interfaces;

namespace Taav.Contracts.BaseClass;

public class Entity
{
    private readonly List<IEvent> _events;
    public IReadOnlyList<IEvent> Events => _events.AsReadOnly();

    public Entity()
    {
        _events = new List<IEvent>();
    }

    public void AppendEvent(IEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}
