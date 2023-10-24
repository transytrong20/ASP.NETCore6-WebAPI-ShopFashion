using Shop.Webapp.Shared.Audited;

namespace Shop.Webapp.EFcore.Repositories.Abstracts
{
    public interface IBasicRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task InsertManyAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task UpdateManyAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
    }
}
