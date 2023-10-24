using Microsoft.EntityFrameworkCore;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.Audited;

namespace Shop.Webapp.EFcore.Repositories.Impls
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public ReadOnlyRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        protected AppDbContext Context => _context;
        protected DbSet<TEntity> DataSet => _dbSet;

        public IQueryable<TEntity> AsNoTracking()
        {
            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                var isDeletedProp = typeof(TEntity).GetProperty(nameof(FullAuditedEntity.IsDeleted));
                return _dbSet.AsNoTracking().Where(x => !EF.Property<bool>(x, nameof(FullAuditedEntity.IsDeleted)));
            }

            return _dbSet.AsNoTracking();
        }

        public async Task<TEntity> FindAsync(params object?[]? keyValues)
        {
            var entity = await _dbSet.FindAsync(keyValues);
            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                var isDeletedProp = typeof(TEntity).GetProperty(nameof(FullAuditedEntity.IsDeleted));
                if ((bool)isDeletedProp.GetValue(entity))
                    return null;
            }
            return entity;
        }

        public IQueryable<TEntity> GetQueryable()
        {

            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                var isDeletedProp = typeof(TEntity).GetProperty(nameof(FullAuditedEntity.IsDeleted));
                return _dbSet.AsQueryable().Where(x => !EF.Property<bool>(x, nameof(FullAuditedEntity.IsDeleted)));
            }

            return _dbSet.AsQueryable();
        }
    }
}
