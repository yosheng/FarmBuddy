using FarmBuddy.Service.ThirdApi;

namespace FarmBuddy.Service.Tests.ThirdApi;

public class MoaApiTest
{
    private readonly IMoaApi _moaApi;

    public MoaApiTest(IMoaApi moaApi)
    {
        _moaApi = moaApi;
    }

    [Fact]
    public async Task GetAgriProductsTransTypeAsyncTest()
    {
        var agriProductsTransType = await _moaApi.GetAgriProductsTransTypeAsync(start_time: "114.11.23", end_time: "114.11.23", cropName: "椰子");
        
        Assert.NotNull(agriProductsTransType);
    }
}