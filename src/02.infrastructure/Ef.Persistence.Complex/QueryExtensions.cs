
using Taav.Contracts.Interfaces;

namespace ComplexProject.Persistence.Ef;

public static class QueryExtensions
{
    public static async Task<IPageResult<T>> Paginate<T>(
        this IQueryable<T> query,
        IPagination pagination)
        where T : class
    {
        if (pagination.Limit.HasValue && pagination.Offset.HasValue)
            return await query.Page(pagination);

        return await query.Page();
    }

}