using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service.Handlers;

/// <summary>
/// AI 模型处理器接口，封装不同 AI 模型的操作
/// </summary>
public interface IAiModelHandler
{
    /// <summary>
    /// 获取对应 AI 模型的 ChatCompletionService 实例
    /// </summary>
    IChatCompletionService GetChatCompletionService();

    /// <summary>
    /// 获取该模型的提示执行设置
    /// </summary>
    PromptExecutionSettings GetPromptExecutionSettings();

    /// <summary>
    /// 计算文本的 Token 数（模型特定实现）
    /// </summary>
    int CalculateTokens(string text);
}
