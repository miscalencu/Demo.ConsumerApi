using Demo.API;
using Demo.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Demo.ConsumeAPI
{
    internal class Startup
    {
        public static IServiceProvider ServiceProvider;
        public static IConfigurationRoot? ConfigurationRoot;

        public static void RegisterServices()
        {
            // configure services
            var services = new ServiceCollection()
                .AddScoped<ICommentsService, CommentsService>();

            // configure logger
            services
                .AddLogging(configure =>
                {
                    configure.AddNLog("nlog.config");
                    configure.AddConsole();
                })
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);

            // add configuration
            ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath("appsettings.json"), true, true)
                //.AddUserSecrets<Program>()
                .Build();

            // add services
            services.ConfigureHttpClients(ConfigurationRoot);

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void SetupErrorLogger()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs exception)
        {
            var _logger = ServiceProvider.GetService<ILogger<Program>>();
            _logger.LogError(exception.ExceptionObject as Exception, "An error has occured" + Environment.NewLine);
            _logger.LogWarning("Exiting because an error occured! Please check logs for error details.");
            Environment.Exit(1);
        }
    }
}
