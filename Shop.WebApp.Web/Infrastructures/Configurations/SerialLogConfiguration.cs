using Serilog;

namespace Shop.WebApp.Web.Infrastructures.Configurations
{
    public static class SerialLogConfiguration
    {
        public static void UseSerialLog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
