namespace Taav.Contracts.Interfaces
{
    public interface IPagination
    {
        int? Offset { get; init; }

        int? Limit { get; init; }
    }
}
