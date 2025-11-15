using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

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
        history.AddUserMessage("請你查詢明天臺灣各縣市天氣預報資料及國際都市天氣預報");

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };
        
        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            openAIPromptExecutionSettings,
            kernel: _kernel);

        return result.Content;
    }
}