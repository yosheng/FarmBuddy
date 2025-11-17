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
        var result = await _kernelManager.GetChatMessageContentAsync("你是誰");
        _testOutputHelper.WriteLine(result.Content);
        Assert.NotNull(result);
    }
}