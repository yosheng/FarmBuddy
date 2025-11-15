using FarmBuddy.Common.Enums;
using FarmBuddy.Service.Options;
using FarmBuddy.Service.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Service;

public static class SemanticKernelExtension
{
    public static IServiceCollection AddOpenAiConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOptions<OpenAIOption>()
            .Bind(configuration.GetSection(nameof(OpenAIOption)));
        
        serviceCollection.AddOptions<GeminiOption>()
            .Bind(configuration.GetSection(nameof(GeminiOption)));

        serviceCollection.AddSingleton<IChatCompletionService>(sp =>
        {
            var openAIOption = sp.GetRequiredService<IOptions<OpenAIOption>>().Value;
            var geminiOption = sp.GetRequiredService<IOptions<GeminiOption>>().Value;

            // 根據配置選擇使用的模型
            var aiModelTypeStr = configuration["AiModelType"] ?? "0";
            var aiModelType = Enum.Parse<AiModelType>(aiModelTypeStr);

            return aiModelType switch
            {
                AiModelType.OpenAI => new OpenAIChatCompletionService(openAIOption.ChatModelId, openAIOption.ApiKey),
                AiModelType.Gemini => new GoogleAIGeminiChatCompletionService(geminiOption.ChatModelId, geminiOption.ApiKey),
                _ => new OpenAIChatCompletionService(openAIOption.ChatModelId, openAIOption.ApiKey)
            };
        });
        
        serviceCollection.AddSingleton<KernelPlugin>(sp =>
            KernelPluginFactory.CreateFromType<WeatherForecastPlugin>(serviceProvider: sp));
        serviceCollection.AddKernel();
        
        return serviceCollection;
    }
}