using FarmBuddy.Common.Enums;
using FarmBuddy.Service.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FarmBuddy.Service.Factories;

/// <summary>
/// AI 模型处理器工厂 - 根据模型类型创建对应的处理器
/// </summary>
public static class AiModelHandlerFactory
{
    /// <summary>
    /// 根据 AI 模型类型创建对应的处理器实例
    /// </summary>
    /// <param name="modelType">AI 模型类型</param>
    /// <param name="serviceProvider">依赖注入容器，用于处理器的依赖解析</param>
    /// <returns>对应的 IAiModelHandler 实例</returns>
    /// <exception cref="NotSupportedException">当模型类型不受支持时抛出</exception>
    public static IAiModelHandler CreateHandler(AiModelType modelType, IServiceProvider serviceProvider) =>
        modelType switch
        {
            AiModelType.OpenAI => ActivatorUtilities.CreateInstance<OpenAiModelHandler>(serviceProvider),
            AiModelType.Gemini => ActivatorUtilities.CreateInstance<GeminiModelHandler>(serviceProvider),
            _ => throw new NotSupportedException($"Model type {modelType} is not supported")
        };
}
