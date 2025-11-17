using FarmBuddy.Common.Enums;
using FarmBuddy.Service.Factories;
using FarmBuddy.Service.Handlers;
using FarmBuddy.Service.Options;
using FarmBuddy.Service.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service;

public static class SemanticKernelExtension
{
    public static IServiceCollection AddOpenAiConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOptions<KernelConfig>()
            .Bind(configuration.GetSection(nameof(KernelConfig)));

        serviceCollection.AddOptions<OpenAIOption>()
            .Bind(configuration.GetSection(nameof(OpenAIOption)));

        serviceCollection.AddOptions<GeminiOption>()
            .Bind(configuration.GetSection(nameof(GeminiOption)));

        // 注册 AI 模型处理器，通过工厂根据配置创建对应的处理器实例
        serviceCollection.AddSingleton<IAiModelHandler>(sp =>
        {
            var kernelConfig = sp.GetRequiredService<IOptions<KernelConfig>>().Value;
            return AiModelHandlerFactory.CreateHandler(kernelConfig.AiModelType, sp);
        });

        // ChatCompletionService 从处理器获取
        serviceCollection.AddSingleton<IChatCompletionService>(sp =>
            sp.GetRequiredService<IAiModelHandler>().GetChatCompletionService());

        serviceCollection.AddSingleton<KernelPlugin>(sp =>
            KernelPluginFactory.CreateFromType<WeatherForecastPlugin>(serviceProvider: sp));
        serviceCollection.AddKernel();

        return serviceCollection;
    }
}