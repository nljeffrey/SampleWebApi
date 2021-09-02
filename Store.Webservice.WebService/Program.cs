using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Store.Webservice.WebService
{
    /// <summary>
    /// Entry point of our console app.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Creates the web server.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            const string logPath = "logs\\";

            // Basic logging to sub directory logs (no limits)
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .WriteTo.Console()
                .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a host using builder pattern.
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>Web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}