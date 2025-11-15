using FarmBuddy.Service.Options;
using FarmBuddy.Service.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Service;

public static class SemanticKernelExtension
{
    public static IServiceCollection AddOpenAiConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Get configuration
        serviceCollection.AddOptions<OpenAIOption>()
            .Bind(configuration.GetSection(nameof(OpenAIOption)));

        serviceCollection.AddSingleton<IChatCompletionService>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<OpenAIOption>>().Value;
            return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
        });

        serviceCollection.AddSingleton<KernelPlugin>(sp =>
            KernelPluginFactory.CreateFromType<WeatherForecastPlugin>(serviceProvider: sp));
        serviceCollection.AddKernel();
        
        return serviceCollection;
    }
}