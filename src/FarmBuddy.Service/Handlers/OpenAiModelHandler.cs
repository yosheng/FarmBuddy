using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using FarmBuddy.Service.Options;

namespace FarmBuddy.Service.Handlers;

/// <summary>
/// OpenAI 模型处理器
/// </summary>
public class OpenAiModelHandler : IAiModelHandler
{
    private readonly OpenAIOption _openAIOption;

    public OpenAiModelHandler(IOptions<OpenAIOption> openAIOption)
    {
        _openAIOption = openAIOption.Value;
    }

    public IChatCompletionService GetChatCompletionService() =>
        new OpenAIChatCompletionService(_openAIOption.ChatModelId, _openAIOption.ApiKey);

    public PromptExecutionSettings GetPromptExecutionSettings() =>
        new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        };

    public int CalculateTokens(string text)
    {
        // TODO: 实现 OpenAI 特定的 Token 计算逻辑
        throw new NotImplementedException("OpenAI Token 计算功能尚未实现");
    }
}
