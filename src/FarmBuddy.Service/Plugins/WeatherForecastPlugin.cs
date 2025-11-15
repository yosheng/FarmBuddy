using System.ComponentModel;
using AIDotNet.Toon;
using FarmBuddy.Service.Dtos;
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
    public async Task<List<OptimizedWeatherForecast>> GetGeneralWeatherForecastAsync()
    {
        var response = await _cwaApi.GetGeneralWeatherForecastAsync();
        var optimizedForecasts = response.Records.Location
            .Select(TransformLocationToOptimized)
            .ToList();

        return optimizedForecasts;
    }

    /// <summary>
    /// 将原始Location数据转换为按时间维度优化的预报数据
    /// </summary>
    private OptimizedWeatherForecast TransformLocationToOptimized(GeneralWeatherResponse.Location location)
    {
        // 第一步：从元素数组中提取各类别的数据，建立时间→元素值的映射
        var elementMap = new Dictionary<string, Dictionary<string, GeneralWeatherResponse.Time>>();

        foreach (var element in location.WeatherElement)
        {
            elementMap[element.ElementName] = element.Time
                .ToDictionary(t => $"{t.StartTime}|{t.EndTime}", t => t);
        }

        // 第二步：按时间段维度聚合，避免时间字段重复
        var timePeriods = new List<TimePeriodForecast>();
        var uniqueTimeKeys = elementMap.Values
            .SelectMany(dict => dict.Keys)
            .Distinct()
            .OrderBy(k => k)
            .ToList();

        foreach (var timeKey in uniqueTimeKeys)
        {
            var (startTime, endTime) = ParseTimeKey(timeKey);

            var timePeriod = new TimePeriodForecast
            {
                StartTime = startTime,
                EndTime = endTime,
                Wx = ExtractWeatherCondition(elementMap, "Wx", timeKey),
                PoP = ExtractNumberValue(elementMap, "PoP", timeKey),
                MinT = ExtractNumberValue(elementMap, "MinT", timeKey),
                MaxT = ExtractNumberValue(elementMap, "MaxT", timeKey),
                CI = ExtractTextValue(elementMap, "CI", timeKey)
            };

            timePeriods.Add(timePeriod);
        }

        return new OptimizedWeatherForecast
        {
            LocationName = location.LocationName,
            TimePeriods = timePeriods
        };
    }

    private (string startTime, string endTime) ParseTimeKey(string timeKey)
    {
        var parts = timeKey.Split('|');
        return (parts[0], parts[1]);
    }

    private WeatherCondition ExtractWeatherCondition(
        Dictionary<string, Dictionary<string, GeneralWeatherResponse.Time>> elementMap,
        string elementName,
        string timeKey)
    {
        if (!elementMap.TryGetValue(elementName, out var timeDict) ||
            !timeDict.TryGetValue(timeKey, out var timeData))
        {
            return null;
        }

        return new WeatherCondition
        {
            Description = timeData.Parameter?.ParameterName,
            Code = timeData.Parameter?.ParameterValue
        };
    }

    private NumberValue ExtractNumberValue(
        Dictionary<string, Dictionary<string, GeneralWeatherResponse.Time>> elementMap,
        string elementName,
        string timeKey)
    {
        if (!elementMap.TryGetValue(elementName, out var timeDict) ||
            !timeDict.TryGetValue(timeKey, out var timeData))
        {
            return null;
        }

        return new NumberValue
        {
            Value = timeData.Parameter?.ParameterName,
            Unit = timeData.Parameter?.ParameterUnit
        };
    }

    private TextValue ExtractTextValue(
        Dictionary<string, Dictionary<string, GeneralWeatherResponse.Time>> elementMap,
        string elementName,
        string timeKey)
    {
        if (!elementMap.TryGetValue(elementName, out var timeDict) ||
            !timeDict.TryGetValue(timeKey, out var timeData))
        {
            return null;
        }

        return new TextValue
        {
            Value = timeData.Parameter?.ParameterName
        };
    }
}