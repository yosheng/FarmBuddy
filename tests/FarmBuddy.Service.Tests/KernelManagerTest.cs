using LineMessaging;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service.Tests;

public class KernelManagerTest
{
    private readonly IKernelManager _kernelManager;
    private readonly ITestOutputHelper _testOutputHelper;

    public KernelManagerTest(IKernelManager kernelManager, ITestOutputHelper testOutputHelper)
    {
        _kernelManager = kernelManager;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetChatResultTest()
    {
        var history = new ChatHistory();
        history.AddUserMessage("你是誰");
        var result = await _kernelManager.GetChatMessageContentAsync(history);
        _testOutputHelper.WriteLine(result.Content);
        Assert.NotNull(result);
    }
}