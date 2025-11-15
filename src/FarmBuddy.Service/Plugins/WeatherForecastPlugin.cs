using System.ComponentModel;
using AIDotNet.Toon;
using FarmBuddy.Service.ThirdApi;
using FarmBuddy.Service.ThirdApi.Cwa.Response;
using Microsoft.SemanticKernel;

namespace FarmBuddy.Service.Plugins;

public class WeatherForecastPlugin
{
    private ICwaApi _cwaApi;

    public WeatherForecastPlugin(ICwaApi cwaApi)
    {
        _cwaApi = cwaApi;
    }
    
    [KernelFunction("get_general_weather_forecast")]
    [Description("一般天氣預報-今明 36 小時天氣預報 - 臺灣各縣市天氣預報資料及國際都市天氣預報")]
    public async Task<string> GetGeneralWeatherForecastAsync()
    {
        var response = await _cwaApi.GetGeneralWeatherForecastAsync();

        var result = ToonSerializer.Serialize(response.Records.Location, new ToonSerializerOptions
        {
            Indent = 2,
            Delimiter = ToonDelimiter.COMMA,
            Strict = true,
            LengthMarker = null
        });

        return result;
    }
}