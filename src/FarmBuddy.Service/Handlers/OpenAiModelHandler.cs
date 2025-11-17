using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using FarmBuddy.Service.Options;
using Microsoft.ML.Tokenizers;

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
        var tokenizer = TiktokenTokenizer.CreateForModel(_openAIOption.ChatModelId);
        return tokenizer.CountTokens(text);
    }
}
