using System.Linq.Expressions;
using FarmBuddy.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Repository;

public static class LinqExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<T> WhereIf<T>(
        this IEnumerable<T> source,
        bool condition,
        Func<T, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static async Task<PagingResult<T>> ToPagingResultAsync<T>(this IQueryable<T> query, PagingQueryBase queryBase)
    {
        var total = await query.CountAsync();
        var items = await query
            .Skip(queryBase.Current * queryBase.PageSize)
            .Take(queryBase.PageSize)
            .ToListAsync();

        return new PagingResult<T>
        {
            Items = items,
            Current = queryBase.Current + 1,
            PageSize = queryBase.PageSize,
            Total = total
        };
    }
}