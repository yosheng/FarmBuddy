using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service.Services;

public interface IChatService
{
    Task<string> GetChatResult();
}

public class ChatService : IChatService
{
    private readonly Kernel _kernel;

    public ChatService(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> GetChatResult()
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        // Create a history store the conversation
        var history = new ChatHistory();
        history.AddUserMessage("你是誰");

        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            kernel: _kernel);

        return result.Content;
    }
}