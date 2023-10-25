using Shop.Webapp.Shared.ApiModels.Requests;
using Shop.Webapp.Shared.Audited;
using System.Linq.Expressions;

namespace System.Linq;
public static class LinqExtention
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        if (condition)
            return query.Where(predicate);
        return query;
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, GenericPagingRequest request) where T : Entity
    {
        return OrderBy(query, request.SortBy, request.Direction);
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string? sortBy = "", string? direction = "desc") where T : Entity
    {
        var parametro = Expression.Parameter(typeof(T), "r");
        if (string.IsNullOrEmpty(sortBy))
        {
            if (typeof(T).IsSubclassOf(typeof(FullAuditedEntity)))
                sortBy = nameof(FullAuditedEntity.CreatedTime);
            else
                sortBy = nameof(Entity.Id);
        }
        var expressao = Expression.Property(parametro, sortBy);
        var lambda = Expression.Lambda(expressao, parametro);

        var tipo = typeof(T).GetProperties().FirstOrDefault(x => x.Name.ToLower() == sortBy.ToLower()).PropertyType;


        var nome = "OrderBy";
        if (string.Equals(direction, "desc", StringComparison.InvariantCultureIgnoreCase))
        {
            nome = "OrderByDescending";
        }
        var metodo = typeof(Queryable).GetMethods().First(m => m.Name == nome && m.GetParameters().Length == 2);
        var metodoGenerico = metodo.MakeGenericMethod(new[] { typeof(T), tipo });
        return metodoGenerico.Invoke(query, new object[] { query, lambda }) as IOrderedQueryable<T>;
    }

    public static IQueryable<T> PageBy<T>(this IOrderedQueryable<T> query, GenericPagingRequest filter)
    {
        if (filter.PageSize <= 0)
            filter.PageSize = 10;
        var skipCount = filter.PageIndex > 0 ? (filter.PageIndex - 1) * filter.PageSize : 0;
        return query.Skip(skipCount).Take(filter.PageSize);
    }
}
