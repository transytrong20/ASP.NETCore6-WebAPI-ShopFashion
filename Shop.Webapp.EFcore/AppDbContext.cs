using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Configurations;

namespace Shop.Webapp.EFcore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PermissionGrant> PermissionGrants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AuthenticationConfiguration();
        }
    }
}
