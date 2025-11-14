using FarmBuddy.Service.ThirdApi;

namespace FarmBuddy.Service.Tests.ThirdApi;

public class CwaApiTest
{
    private readonly ICwaApi _cwaApi;

    public CwaApiTest(ICwaApi cwaApi)
    {
        _cwaApi = cwaApi;
    }

    [Fact]
    public async Task GetChanghua3DayForecastAsyncTest()
    {
        var changhua3DayForecast = await _cwaApi.GetChanghua3DayForecastAsync();
        
        Assert.NotNull(changhua3DayForecast);
    }
    
    [Fact]
    public async Task GetGeneralWeatherForecastAsync()
    {
        var generalWeatherForecast = await _cwaApi.GetGeneralWeatherForecastAsync();
        
        Assert.NotNull(generalWeatherForecast);
    }
}