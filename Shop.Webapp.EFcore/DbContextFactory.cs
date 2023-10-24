using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Shop.Webapp.EFcore
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>, IDisposable
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);

            return new AppDbContext(builder.Options);
        }

        public static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Shop.Webapp.Web/"))
                .AddJsonFile("appsettings.Development.json", optional: false);

            return builder.Build();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
