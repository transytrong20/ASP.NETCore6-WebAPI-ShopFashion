using Shop.Webapp.Domain;

namespace Shop.Webapp.Application.Auth.Abstracts
{
    public interface IUserManager
    {
        IQueryable<User> Users { get; }
        Task<User> FindAsync(Guid id);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string username);
        Task<User> CreateAsync(User user, string password);
        Task<User> UpdateAsync(User user);
        Task<bool> IsInRoleAsync(User user, string role);
        Task<string[]> GetRoleByUserAsync(User user);
        Task AddRoleToUserAsync(User user, string role);
        Task RemoveRoleToUserAsync(User user, string role);
        Task RemoveAsync(User user);
        Task<bool> CheckEmailUsedAsync(string email);
        Task<bool> CheckPhoneUsedAsync(string phone);
        Task<bool> CheckExistUserAsync(string username);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> CheckAllowLoginAsync(User user);
        Task LockedUserAsync(User user);
        Task LockedUserAsync(User user, int minutes);
    }
}
