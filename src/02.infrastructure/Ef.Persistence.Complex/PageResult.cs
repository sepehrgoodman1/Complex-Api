using System.Collections.Generic;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Persistence.Ef;

class PageResult<T> : IPageResult<T>
    where T : class
{
    public IEnumerable<T> Elements { get; init; }
    public int TotalElements { get; init; }

    public PageResult(IEnumerable<T> elements, int totalElements)
    {
        Elements = elements;
        TotalElements = totalElements;
    }
}
