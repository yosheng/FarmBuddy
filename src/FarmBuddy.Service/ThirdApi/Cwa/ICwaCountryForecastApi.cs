using FarmBuddy.Service.ThirdApi.Cwa.Response;
using Refit;

namespace FarmBuddy.Service.ThirdApi.Cwa;

public interface ICwaCountryForecastApi
{
    /// <summary>
    /// 鄉鎮天氣預報-宜蘭縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-001")]
    Task<ObservationResponse> GetYilan3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-宜蘭縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-003")]
    Task<ObservationResponse> GetYilanWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-桃園市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-005")]
    Task<ObservationResponse> GetTaoyuan3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-桃園市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-007")]
    Task<ObservationResponse> GetTaoyuanWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新竹縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-009")]
    Task<ObservationResponse> GetHsinchuCounty3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新竹縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-011")]
    Task<ObservationResponse> GetHsinchuCountyWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-苗栗縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-013")]
    Task<ObservationResponse> GetMiaoli3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-苗栗縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-015")]
    Task<ObservationResponse> GetMiaoliWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-彰化縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-017")]
    Task<ObservationResponse> GetChanghua3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-彰化縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-019")]
    Task<ObservationResponse> GetChanghuaWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-南投縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-021")]
    Task<ObservationResponse> GetNantou3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-南投縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-023")]
    Task<ObservationResponse> GetNantouWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-雲林縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-025")]
    Task<ObservationResponse> GetYunlin3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-雲林縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-027")]
    Task<ObservationResponse> GetYunlinWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-嘉義縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-029")]
    Task<ObservationResponse> GetChiayiCounty3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-嘉義縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-031")]
    Task<ObservationResponse> GetChiayiCountyWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-屏東縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-033")]
    Task<ObservationResponse> GetPingtung3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-屏東縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-035")]
    Task<ObservationResponse> GetPingtungWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺東縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-037")]
    Task<ObservationResponse> GetTaitung3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺東縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-039")]
    Task<ObservationResponse> GetTaitungWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-花蓮縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-041")]
    Task<ObservationResponse> GetHualien3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-花蓮縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-043")]
    Task<ObservationResponse> GetHualienWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-澎湖縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-045")]
    Task<ObservationResponse> GetPenghu3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-澎湖縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-047")]
    Task<ObservationResponse> GetPenguWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-基隆市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-049")]
    Task<ObservationResponse> GetKeelung3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-基隆市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-051")]
    Task<ObservationResponse> GetKeelungWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新竹市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-053")]
    Task<ObservationResponse> GetHsinchuCity3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新竹市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-055")]
    Task<ObservationResponse> GetHsinchuCityWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-嘉義市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-057")]
    Task<ObservationResponse> GetChiayiCity3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-嘉義市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-059")]
    Task<ObservationResponse> GetChiayiCityWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺北市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-061")]
    Task<ObservationResponse> GetTaipei3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺北市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-063")]
    Task<ObservationResponse> GetTaipeiWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-高雄市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-065")]
    Task<ObservationResponse> GetKaohsiung3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-高雄市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-067")]
    Task<ObservationResponse> GetKaohsiungWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新北市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-069")]
    Task<ObservationResponse> GetNewTaipei3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-新北市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-071")]
    Task<ObservationResponse> GetNewTaipeiWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺中市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-073")]
    Task<ObservationResponse> GetTaichung3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺中市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-075")]
    Task<ObservationResponse> GetTaichungWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺南市未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-077")]
    Task<ObservationResponse> GetTainan3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-臺南市未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-079")]
    Task<ObservationResponse> GetTainanWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-連江縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-081")]
    Task<ObservationResponse> GetLianjiang3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-連江縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-083")]
    Task<ObservationResponse> GetLianjiangWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-金門縣未來3天天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-085")]
    Task<ObservationResponse> GetKinmen3DayForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);

    /// <summary>
    /// 鄉鎮天氣預報-金門縣未來1週天氣預報
    /// </summary>
    [Get("/v1/rest/datastore/F-D0047-087")]
    Task<ObservationResponse> GetKinmenWeeklyForecastAsync(
        [Query] string? locationName = null,
        [Query] string? elementName = null,
        [Query] string? format = "JSON",
        [Query] int? limit = null,
        [Query] int? offset = null);
}