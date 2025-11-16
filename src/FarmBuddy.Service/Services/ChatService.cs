using LineMessaging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FarmBuddy.Service.Services;

public interface IChatService
{
    Task<string> GetLineChatResult(LineWebhookContent lineWebhookContent);
}

public class ChatService : IChatService
{
    private readonly Kernel _kernel;

    public ChatService(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> GetLineChatResult(LineWebhookContent lineWebhookContent)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        if (!lineWebhookContent.Events.Any())
        {
            return "OK";
        }

        var history = new ChatHistory();
        history.AddUserMessage(lineWebhookContent.Events.First().Message.Text);

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        };

        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            openAIPromptExecutionSettings,
            kernel: _kernel);

        return result.Content;
    }
}