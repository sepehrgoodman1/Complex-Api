namespace Taav.Contracts.Interfaces;

public interface DateTimeService : Service
{
    DateTime Now { get; }
    DateTime Today { get; }
    DateTime ToLocal(DateTime time);
}