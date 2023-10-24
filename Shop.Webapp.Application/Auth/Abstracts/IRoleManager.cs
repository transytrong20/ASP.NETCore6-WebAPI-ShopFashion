using Shop.Webapp.Domain;

namespace Shop.Webapp.Application.Auth.Abstracts
{
    public interface IRoleManager
    {
        IQueryable<Role> Roles { get; }
        Task<Role> FindAsync(Guid id);
        Task<Role> CreateAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(Role role);
    }
}
