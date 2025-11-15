using FarmBuddy.Service.Plugins;

namespace FarmBuddy.Service.Tests.Plugins;

public class WeatherForecastPluginTest
{
    private readonly WeatherForecastPlugin _weatherForecastPlugin;

    public WeatherForecastPluginTest(WeatherForecastPlugin weatherForecastPlugin)
    {
        _weatherForecastPlugin = weatherForecastPlugin;
    }
    
    [Fact]
    public async Task GetGeneralWeatherForecastAsyncTest()
    {
        var result = await _weatherForecastPlugin.GetGeneralWeatherForecastAsync();
        Assert.NotNull(result);
    }
}