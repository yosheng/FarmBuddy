using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using FarmBuddy.Service.Options;
using Microsoft.ML.Tokenizers;

namespace FarmBuddy.Service.Handlers;

/// <summary>
/// Google Gemini 模型处理器
/// </summary>
public class GeminiModelHandler : IAiModelHandler
{
    private readonly GeminiOption _geminiOption;

    public GeminiModelHandler(IOptions<GeminiOption> geminiOption)
    {
        _geminiOption = geminiOption.Value;
    }

    public IChatCompletionService GetChatCompletionService() =>
        new GoogleAIGeminiChatCompletionService(_geminiOption.ChatModelId, _geminiOption.ApiKey);

    public PromptExecutionSettings GetPromptExecutionSettings() =>
        new GeminiPromptExecutionSettings()
        {
            ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
        };

    public int CalculateTokens(string text)
    {
        var tokenizer = TiktokenTokenizer.CreateForModel(_geminiOption.ChatModelId);
        return tokenizer.CountTokens(text);
    }
}
