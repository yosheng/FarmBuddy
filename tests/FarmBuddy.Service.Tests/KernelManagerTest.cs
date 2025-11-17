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
    
    [Fact]
    public async Task Test_Ask_Taiwan_Weather()
    {
        var result = await _kernelManager.GetChatMessageContentAsync("請你告訴我雲林縣土庫鎮中水稻這兩天的天氣適合收成嗎?");
        _testOutputHelper.WriteLine(result.Content);
        Assert.NotNull(result);
    }
}