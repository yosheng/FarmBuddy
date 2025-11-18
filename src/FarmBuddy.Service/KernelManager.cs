using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Enums;
using FarmBuddy.Repository;
using FarmBuddy.Service.Handlers;
using FarmBuddy.Service.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service;

public interface IKernelManager
{
    Task<ChatMessageContent> GetChatMessageContentAsync(string userId, string userMessage);
}

public class KernelManager : IKernelManager
{
    private readonly Kernel _kernel;
    private readonly IOptions<KernelConfig> _kernelConfig;
    private readonly IAiModelHandler _modelHandler;
    private readonly FarmBuddyDbContext _dbContext;
    
    public KernelManager(Kernel kernel, IOptions<KernelConfig> kernelConfig, IAiModelHandler modelHandler,
        FarmBuddyDbContext dbContext)
    {
        _kernel = kernel;
        _kernelConfig = kernelConfig;
        _modelHandler = modelHandler;
        _dbContext = dbContext;
    }

    public async Task<ChatMessageContent> GetChatMessageContentAsync(string userId, string userMessage)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = _modelHandler.GetPromptExecutionSettings();

        var history = new ChatHistory();
        history.AddSystemMessage(_kernelConfig.Value.SystemMessage);
        history.AddAssistantMessage(_kernelConfig.Value.AssistantMessage);

        var chatHistory = await _dbContext.ChatMessages
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreateTime)
            .Take(5)
            .ToListAsync();

        foreach (var message in chatHistory
                     .OrderBy(c => c.CreateTime))
        {
            history.Add(new ChatMessageContent(
                message.Role == ChatRoleType.User ? AuthorRole.User : AuthorRole.Assistant,
                message.Content));
        }

        history.AddUserMessage(userMessage);

        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            promptExecutionSettings,
            kernel: _kernel);

        if (result.Content != null) await SaveChatMessageAsync(userId, userMessage, result.Content);
        

        return result;
    }

    private async Task SaveChatMessageAsync(string userId, string userMessage, string assistantMessage)
    {
        var userChatMessage = new ChatMessage
        {
            UserId = userId,
            Role = ChatRoleType.User,
            Content = userMessage,
            CreateTime = DateTime.UtcNow
        };

        var assistantChatMessage = new ChatMessage
        {
            UserId = userId,
            Role = ChatRoleType.Assistant,
            Content = assistantMessage,
            CreateTime = DateTime.UtcNow
        };

        _dbContext.ChatMessages.Add(userChatMessage);
        _dbContext.ChatMessages.Add(assistantChatMessage);
        await _dbContext.SaveChangesAsync();
    }
}