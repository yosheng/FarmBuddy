using FarmBuddy.Api.Options;
using FarmBuddy.Service.Plugins;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Api;

public static class SemanticKernelExtension
{
    public static IServiceCollection AddOpenAiConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Get configuration
        serviceCollection.AddOptions<OpenAIOption>()
            .Bind(configuration.GetSection(nameof(OpenAIOption)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        serviceCollection.AddSingleton<IChatCompletionService>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<OpenAIOption>>().Value;
            return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
        });

        serviceCollection.AddSingleton<WeatherForecastPlugin>();
        serviceCollection.AddTransient<Kernel>((sp) =>
        {
            // Create a collection of plugins that the kernel will use
            KernelPluginCollection pluginCollection = new();
            
            pluginCollection.AddFromObject(sp.GetRequiredService<WeatherForecastPlugin>());
            
            /* 添加對應插件
pluginCollection.AddFromObject(sp.GetRequiredKeyedService<MyLightPlugin>("OfficeLight"), "OfficeLight");
pluginCollection.AddFromObject(sp.GetRequiredKeyedService<MyLightPlugin>("PorchLight"), "PorchLight");*/

            return new Kernel(sp, pluginCollection);
        });

        return serviceCollection;
    }
}