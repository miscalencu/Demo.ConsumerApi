using Demo.API.Helpers;
using Demo.API.Models;
using Demo.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.ConsumeAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Startup.RegisterServices();
            Startup.SetupErrorLogger();

            Console.WriteLine("Starting console app.");

            var _config = Startup.ConfigurationRoot.Get<ClientsConfiguration>();
            var _logger = Startup.ServiceProvider.GetService<ILogger<Program>>();
            var _commentsService = Startup.ServiceProvider.GetService<ICommentsService>();

            _logger.LogInformation("Stating console app.");

            try 
            {
                // add brakepoint here
                var comments = await _commentsService.GetAll();
                Console.WriteLine($"{comments.Count} comments retrieved.");
                _logger.LogInformation($"{comments.Count} comments retrieved.");

                var comment = await _commentsService.GetById(10);
                Console.WriteLine($"Comment with Id=10 retrieved: {SerializationHelper.ToJson(comment)}");
                _logger.LogInformation($"Comment with Id=10 retrieved: {SerializationHelper.ToJson(comment)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing app.");
                Console.WriteLine("Error occured. Check error files for details.");
            }

            _logger.LogInformation("Closing console app.");

            Console.WriteLine("Closing console app. Press any key to close");
            Console.ReadKey();
        }
    }
}