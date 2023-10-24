using Microsoft.EntityFrameworkCore;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.Audited;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Shop.Webapp.EFcore.Repositories.Impls
{
    public class Repository<TEntity> : BasicRepository<TEntity>, IRepository<TEntity>, IReadOnlyRepository<TEntity>, IBasicRepository<TEntity> where TEntity : Entity
    {
        public Repository(AppDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        public async Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var lst = GetQueryable().Where(predicate);
            if (lst.Any())
                await DeleteManyAsync(lst);
        }

        public IQueryable<TEntity> ExecuteQuery(string query, object? parameters)
        {
            var sqlParameters = new List<SqlParameter>();
            if (parameters != null)
            {
                var properties = parameters.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    var propName = prop.Name;
                    var propValue = parameters.GetType().GetProperty(propName).GetValue(parameters);

                    sqlParameters.Add(new SqlParameter(propName, propValue));
                }
            }
            return DataSet.FromSqlRaw(query, sqlParameters.ToArray());
        }

        public bool IsTrackedEntity(TEntity entity)
        {
            return DataSet.Local.Contains(entity);
        }
    }
}
