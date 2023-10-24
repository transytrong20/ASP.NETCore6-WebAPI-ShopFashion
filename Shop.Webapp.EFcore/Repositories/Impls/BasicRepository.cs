using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.Audited;
using System.Data;

namespace Shop.Webapp.EFcore.Repositories.Impls
{
    public class BasicRepository<TEntity> : ReadOnlyRepository<TEntity>, IBasicRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : Entity
    {
        protected IServiceProvider ServiceProvider { get; }
        protected ICurrentUser CurrentUser => ServiceProvider.GetRequiredService<ICurrentUser>();
        private IUnitOfWork UnitOfWork => ServiceProvider.GetRequiredService<IUnitOfWork>();

        public BasicRepository(AppDbContext context, IServiceProvider serviceProvider) : base(context)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.IsDeleted)).SetValue(entity, true);
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.DeletedTime)).SetValue(entity, DateTime.Now);
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.DeletedBy)).SetValue(entity, CurrentUser.Id);

                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                DataSet.Remove(entity);
            }

            await SaveChangesAsync();
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            var arr = entities.ToArray();

            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                foreach (TEntity entity in arr)
                {
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.IsDeleted)).SetValue(entity, true);
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.DeletedTime)).SetValue(entity, DateTime.Now);
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.DeletedBy)).SetValue(entity, CurrentUser.Id);

                    Context.Entry(entity).State = EntityState.Modified;
                }
            }
            else
            {
                DataSet.RemoveRange(arr);
            }

            await SaveChangesAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            SetKeyAsDefault(entity);

            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.CreatedTime)).SetValue(entity, DateTime.Now);
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.CreatedBy)).SetValue(entity, CurrentUser.Id);
            }

            var result = (await DataSet.AddAsync(entity)).Entity;

            await SaveChangesAsync();

            return result;
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            var arr = entities.ToArray();

            foreach (TEntity entity in arr)
            {
                SetKeyAsDefault(entity);

                if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
                {
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.CreatedTime)).SetValue(entity, DateTime.Now);
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.CreatedBy)).SetValue(entity, CurrentUser.Id);
                }
            }

            await DataSet.AddRangeAsync(arr);


            await SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
            {
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.LastModifiedTime)).SetValue(entity, DateTime.Now);
                typeof(TEntity).GetProperty(nameof(FullAuditedEntity.LastModifiedBy)).SetValue(entity, CurrentUser.Id);
            }

            Context.Entry(entity).State = EntityState.Modified;


            await SaveChangesAsync();

            return entity;
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            var arr = entities.ToArray();

            foreach (var entity in arr)
            {
                if (typeof(TEntity).IsSubclassOf(typeof(FullAuditedEntity)))
                {
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.LastModifiedTime)).SetValue(entity, DateTime.Now);
                    typeof(TEntity).GetProperty(nameof(FullAuditedEntity.LastModifiedBy)).SetValue(entity, CurrentUser.Id);
                }

                Context.Entry(entity).State = EntityState.Modified;
            }


            await SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            if (UnitOfWork.InTransaction)
                return Task.CompletedTask;

            return Context.SaveChangesAsync();
        }

        protected void SetKeyAsDefault(TEntity entity)
        {
            if (entity.Id == default)
                typeof(TEntity).GetProperty(nameof(Entity.Id)).SetValue(entity, Guid.NewGuid());
        }
    }
}
