using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TGC.WIQLQueryBuilder.Services;

namespace TGC.WIQLQueryBuilder;
public static class IoCExtension
{
    public static void AddAzureServices(this ServiceCollection collection)
    {
        
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        collection.Configure<AzureSettings>(configuration.GetSection("AzureSettings"));

        collection.AddScoped<AzureClientService>();
        collection.AddScoped<AzureWorkItemsService>();
    }
}
