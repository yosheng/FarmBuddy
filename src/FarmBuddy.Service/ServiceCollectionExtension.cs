using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FarmBuddy.Service.ThirdApi;
using FarmBuddy.Service.ThirdApi.Cwa;
using Microsoft.Extensions.Configuration;
using Refit;

namespace FarmBuddy.Service;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var assembly = typeof(ServiceCollectionExtension).Assembly;
        var types = assembly
            .GetTypes()
            .Where(t =>
                !t.IsGenericType &&
                !t.IsAbstract &&
                t.IsClass &&
                t.Name.EndsWith("Service") &&
                !t.Name.EndsWith("ApiService"))
            .ToList();
        foreach (var type in types)
        {
            var baseType = type.GetInterfaces().FirstOrDefault(t => t.Name == $"I{type.Name}");
            serviceCollection.TryAddScoped(baseType, type);
        }
        
        serviceCollection.AddTransient<CwaAuthorizationHandler>(_ => new CwaAuthorizationHandler(configuration["Endpoint:CwaApiKey"]!));
        
        serviceCollection
            .AddRefitClient<ICwaApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Endpoint:CwaApi"]!))
            .AddHttpMessageHandler<CwaAuthorizationHandler>();

        return serviceCollection;
    }
}