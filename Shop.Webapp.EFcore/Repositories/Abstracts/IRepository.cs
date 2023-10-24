using Shop.Webapp.Shared.Audited;
using System.Linq.Expressions;

namespace Shop.Webapp.EFcore.Repositories.Abstracts
{
    public interface IRepository<TEntity> : IBasicRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : Entity
    {
        Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> ExecuteQuery(string query, object? paramemters);
        bool IsTrackedEntity(TEntity entity);
    }
}
