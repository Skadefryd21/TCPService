using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TCPService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // This method sets up the host builder, which configures the services and logging, and starts the application.
        // Think of the Host as a container that contains the running service.
        // To setup the configuration context look at code from Line: 20 to Line: 24.
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
                {
                    // Register the Worker service, so it's managed by the host.
                    services.AddHostedService<Worker>();

                    //Registers logging services for Dependancy Injection
                    services.AddLogging(configure => 
                    configure.AddConsole()
                    );
                });
    }
}
