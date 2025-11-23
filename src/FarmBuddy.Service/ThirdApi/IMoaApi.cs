using FarmBuddy.Service.ThirdApi.Moa.Response;
using Refit;

namespace FarmBuddy.Service.ThirdApi;

/// <summary>
/// 農委會開放資料API
/// </summary>
[Headers("Accept: application/json")]
public interface IMoaApi
{
    /// <summary>
    /// 獲取農產品交易行情
    /// </summary>
    /// <param name="start_time">交易日期(起) 格式: ROC.MM.DD (如107.07.01)</param>
    /// <param name="end_time">交易日期(迄) 格式: ROC.MM.DD (如107.07.10)</param>
    /// <param name="cropCode">農產品代碼</param>
    /// <param name="cropName">農產品名稱</param>
    /// <param name="marketName">市場名稱</param>
    /// <param name="page">頁碼控制</param>
    /// <param name="tcType">農產品種類代碼</param>
    /// <returns>農產品交易行情列表</returns>
    [Get("/AgriProductsTransType/")]
    Task<MoaResponse<AgriProductsTransTypeResponse>> GetAgriProductsTransTypeAsync(
        [Query] string? start_time = null,
        [Query] string? end_time = null,
        [Query] string? cropCode = null,
        [Query] string? cropName = null,
        [Query] string? marketName = null,
        [Query] string? page = null,
        [Query(CollectionFormat.Multi)] string? tcType = null);
}
