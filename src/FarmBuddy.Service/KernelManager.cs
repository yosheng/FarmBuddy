using FarmBuddy.Common.Enums;
using FarmBuddy.Service.Options;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Service;

public interface IKernelManager
{
    Task<ChatMessageContent> GetChatMessageContentAsync(string userMessage);
}

public class KernelManager : IKernelManager
{
    private readonly Kernel _kernel;
    private readonly IOptions<KernelConfig> _kernelConfig;

    public KernelManager(Kernel kernel, IOptions<KernelConfig> kernelConfig)
    {
        _kernel = kernel;
        _kernelConfig = kernelConfig;
    }

    public async Task<ChatMessageContent> GetChatMessageContentAsync(string userMessage)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        PromptExecutionSettings promptExecutionSettings = _kernelConfig.Value.AiModelType switch
        {
            AiModelType.OpenAI => new OpenAIPromptExecutionSettings()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            },
            AiModelType.Gemini => new GeminiPromptExecutionSettings()
            {
                ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            },
            _ => new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions, }
        };
        
        var history = new ChatHistory();
        history.AddSystemMessage(_kernelConfig.Value.SystemMessage);
        history.AddAssistantMessage(_kernelConfig.Value.AssistantMessage);
        history.AddUserMessage(userMessage);

        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            promptExecutionSettings,
            kernel: _kernel);

        return result;
    }
}