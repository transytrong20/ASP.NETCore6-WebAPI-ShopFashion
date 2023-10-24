using Shop.Webapp.Shared.Audited;

namespace Shop.Webapp.EFcore.Repositories.Abstracts
{
    public interface IReadOnlyRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> AsNoTracking();
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> FindAsync(params object?[]? keyValues);
    }
}
