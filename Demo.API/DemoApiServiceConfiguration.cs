using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Demo.API
{
    public static class DemoApiServiceConfiguration
    {
        public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("commentsApi", c =>
            {
                c!.BaseAddress = new Uri(configuration!["apiclients:comments:url"]!);
                c.DefaultRequestHeaders!.Add("User-Agent", "DemoAPI");
            }).AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)
            ));
        }
    }
}