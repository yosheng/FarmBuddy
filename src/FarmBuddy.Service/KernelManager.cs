using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Service;

public interface IKernelManager
{
    Task<ChatMessageContent> GetChatMessageContentAsync(ChatHistory history);
}

public class KernelManager : IKernelManager
{
    private readonly Kernel _kernel;

    public KernelManager(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<ChatMessageContent> GetChatMessageContentAsync(ChatHistory history)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        };

        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            openAIPromptExecutionSettings,
            kernel: _kernel);

        return result;
    }
}