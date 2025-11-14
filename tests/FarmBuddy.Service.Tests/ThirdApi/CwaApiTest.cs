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
    public async Task GetCwaDataAsync()
    {
        var changhua3DayForecast = await _cwaApi.GetGeneralWeatherForecastAsync();
        
        Assert.NotNull(changhua3DayForecast);
    }
}