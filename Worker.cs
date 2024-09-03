using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TCPService
{
    public class Worker : BackgroundService
    {
        // Dependency injection is used to provide this logger to the Worker class.
        private readonly ILogger<Worker> _logger;

        // Logging is crucial for monitoring and debugging background services.
        // Since background services run in the background, logging helps us understand what's happening without needing to attach a debugger.
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        
        // Method that's run by default.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Instantiating TCPServer runs the contructor which contains the StartServer method.
            TCPServer tcpServer = new TCPServer();

            // I've changed ExecuteAsync to define my wanted behavior: logging a message every second until cancellation is requested.
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(message: $"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);
            }
            _logger.LogInformation("Worker is stopping.");
        }
    }
}
