using FarmBuddy.Service.Services;

namespace FarmBuddy.Service.Tests.Services;

public class ChatServiceTest
{
    private readonly IChatService _chatService;

    public ChatServiceTest(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    [Fact]
    public async Task GetChatResultTest()
    {
        var result = await _chatService.GetChatResult();
        
        Assert.NotNull(result);
    }
}