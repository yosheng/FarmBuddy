using FarmBuddy.Service.Services;
using LineMessaging;

namespace FarmBuddy.Service.Tests.Services;

public class ChatServiceTest
{
    private readonly IChatService _chatService;
    private readonly ITestOutputHelper _testOutputHelper;

    public ChatServiceTest(IChatService chatService, ITestOutputHelper testOutputHelper)
    {
        _chatService = chatService;
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GetChatResultTest()
    {
        var result = await _chatService.GetLineChatResult(new LineWebhookContent());
        _testOutputHelper.WriteLine(result);
        Assert.NotNull(result);
    }
}