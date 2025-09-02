using System.Linq.Expressions;

namespace BookCatalog.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string property, bool ascending = true)
    {
        if (string.IsNullOrWhiteSpace(property))
        {
            return query;
        }
        
        var parameter = Expression.Parameter(typeof(T), "x");
        var field = Expression.PropertyOrField(parameter, property);
        var lambda = Expression.Lambda(field, parameter);
        
        return ascending
            ? Queryable.OrderBy(query, (dynamic)lambda)
            : Queryable.OrderByDescending(query, (dynamic)lambda);
    }
}