using FarmBuddy.Service.Services;

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
        var result = await _chatService.GetChatResult();
        _testOutputHelper.WriteLine(result);
        Assert.NotNull(result);
    }
}