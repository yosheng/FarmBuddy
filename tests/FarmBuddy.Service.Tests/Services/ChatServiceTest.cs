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
    

}