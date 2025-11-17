using FarmBuddy.Service.Handlers;
using FarmBuddy.Service.Options;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service;

public interface IKernelManager
{
    Task<ChatMessageContent> GetChatMessageContentAsync(string userMessage);
}

public class KernelManager : IKernelManager
{
    private readonly Kernel _kernel;
    private readonly IOptions<KernelConfig> _kernelConfig;
    private readonly IAiModelHandler _modelHandler;

    public KernelManager(Kernel kernel, IOptions<KernelConfig> kernelConfig, IAiModelHandler modelHandler)
    {
        _kernel = kernel;
        _kernelConfig = kernelConfig;
        _modelHandler = modelHandler;
    }

    public async Task<ChatMessageContent> GetChatMessageContentAsync(string userMessage)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = _modelHandler.GetPromptExecutionSettings();

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