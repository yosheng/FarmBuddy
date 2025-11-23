using System.Text.Json.Serialization;

namespace FarmBuddy.Service.ThirdApi.Moa.Response;

/// <summary>
/// 農產品交易行情回應
/// </summary>
public class AgriProductsTransTypeResponse
{
    /// <summary>交易日期 (ROC日期格式, 如 114.11.23)</summary>
    [JsonPropertyName("TransDate")]
    public string? TransDate { get; set; }

    /// <summary>農產品種類代碼</summary>
    [JsonPropertyName("TcType")]
    public string? TcType { get; set; }

    /// <summary>農產品代碼</summary>
    [JsonPropertyName("CropCode")]
    public string? CropCode { get; set; }

    /// <summary>農產品名稱</summary>
    [JsonPropertyName("CropName")]
    public string? CropName { get; set; }

    /// <summary>市場代號</summary>
    [JsonPropertyName("MarketCode")]
    public string? MarketCode { get; set; }

    /// <summary>市場名稱</summary>
    [JsonPropertyName("MarketName")]
    public string? MarketName { get; set; }

    /// <summary>上價(元/公斤)</summary>
    [JsonPropertyName("Upper_Price")]
    public decimal? UpperPrice { get; set; }

    /// <summary>中價(元/公斤)</summary>
    [JsonPropertyName("Middle_Price")]
    public decimal? MiddlePrice { get; set; }

    /// <summary>下價(元/公斤)</summary>
    [JsonPropertyName("Lower_Price")]
    public decimal? LowerPrice { get; set; }

    /// <summary>平均價(元/公斤)</summary>
    [JsonPropertyName("Avg_Price")]
    public decimal? AvgPrice { get; set; }

    /// <summary>交易量(公斤)</summary>
    [JsonPropertyName("Trans_Quantity")]
    public decimal? TransQuantity { get; set; }
}
