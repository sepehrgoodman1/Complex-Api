using System.ComponentModel.DataAnnotations;
using Taav.Contracts.Interfaces;

namespace Ef.Persistence.ComplexProject;

public class Pagination : IPagination
{
    [Range(0, int.MaxValue)]
    public int? Offset { get; init; }

    [Range(0, int.MaxValue)]
    public int? Limit { get; init; }
}