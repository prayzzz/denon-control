using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DenonControl.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseSerilog(ConfigureLogging)
                       .ConfigureWebHostDefaults(webHostBuilder =>
                       {
                           webHostBuilder.UseStartup<Startup>()
                                         .SuppressStatusMessages(true);
                       });
        }

        private static void ConfigureLogging(HostBuilderContext context, LoggerConfiguration config)
        {
            config.ReadFrom.Configuration(context.Configuration);
        }
    }
}