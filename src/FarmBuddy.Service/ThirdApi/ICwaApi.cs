using FarmBuddy.Service.ThirdApi.Cwa;
using FarmBuddy.Service.ThirdApi.Cwa.Response;

namespace FarmBuddy.Service.ThirdApi;

using Refit;

/// <summary>
/// 中央氣象署開放資料平臺 API
/// https://opendata.cwa.gov.tw/apidoc/v1
/// </summary>
public interface ICwaApi : ICwaCountryForecastApi
{
    /// <summary>
    /// 一般天氣預報-今明 36 小時天氣預報 - 臺灣各縣市天氣預報資料及國際都市天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-C0032-001")]
    Task<GeneralWeatherResponse> GetGeneralWeatherForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null,
        [Query] string? timeFrom = null,
        [Query] string? timeTo = null);
}