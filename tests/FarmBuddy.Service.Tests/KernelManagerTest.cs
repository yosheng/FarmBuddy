using FarmBuddy.Service.Options;
using LineMessaging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service.Tests;

public class KernelManagerTest
{
    private readonly IKernelManager _kernelManager;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IOptions<KernelConfig> _kernelConfig;

    public KernelManagerTest(IKernelManager kernelManager, ITestOutputHelper testOutputHelper,
        IOptions<KernelConfig> kernelConfig)
    {
        _kernelManager = kernelManager;
        _testOutputHelper = testOutputHelper;
        _kernelConfig = kernelConfig;
    }

    [Fact]
    public async Task GetChatResultTest()
    {
        var history = new ChatHistory();
        history.AddSystemMessage(_kernelConfig.Value.SystemMessage);
        history.AddAssistantMessage(_kernelConfig.Value.AssistantMessage);
        history.AddUserMessage("你是誰");
        var result = await _kernelManager.GetChatMessageContentAsync(history);
        _testOutputHelper.WriteLine(result.Content);
        Assert.NotNull(result);
    }
}