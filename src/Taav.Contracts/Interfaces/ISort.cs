namespace Taav.Contracts.Interfaces;

public interface ISort
{
    string? Expression { get; }

    Dictionary<string, string> GetSortParameters();
}
