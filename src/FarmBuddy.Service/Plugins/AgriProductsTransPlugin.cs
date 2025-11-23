using System.ComponentModel;
using System.Globalization;
using FarmBuddy.Service.ThirdApi;
using FarmBuddy.Service.ThirdApi.Moa.Response;
using Microsoft.SemanticKernel;

namespace FarmBuddy.Service.Plugins;

public class AgriProductsTransPlugin
{
    private readonly IMoaApi _moaApi;

    public AgriProductsTransPlugin(IMoaApi moaApi)
    {
        _moaApi = moaApi;
    }

    [KernelFunction("get_today_agri_products_price")]
    [Description("查詢今天的農產品交易行情。使用者必須指定農產品名稱（如：番茄、高麗菜、蘿蔔等），不支持全部查詢。如果使用者沒有指定農產品名稱，請引導使用者提供具體的農產品名稱")]
    public async Task<AgriProductsPriceResult?> GetTodayAgriProductsPriceAsync(
        [Description("農產品名稱，例如：番茄、高麗菜、蘿蔔、洋蔥、青椒等。必須提供具體農產品名稱，不接受『全部』或模糊查詢")]
        string cropName)
    {
        if (string.IsNullOrWhiteSpace(cropName) || cropName.Equals("全部", StringComparison.OrdinalIgnoreCase))
        {
            return new AgriProductsPriceResult
            {
                IsSuccess = false,
                Message = "請提供具體的農產品名稱（例如：番茄、高麗菜、蘿蔔、洋蔥、青椒、冬瓜等），不支持查詢全部農產品。",
                AvailableOptions = "您可以查詢的農產品包括：番茄、高麗菜、蘿蔔、洋蔥、青椒、冬瓜、苦瓜、茼蒿、菠菜、萵苣等常見蔬菜類產品。"
            };
        }
        
        var culture = new CultureInfo("zh-TW")
        {
            DateTimeFormat =
            {
                Calendar = new TaiwanCalendar()
            }
        };

        var today = DateTime.Now.ToString("yyy.MM.dd", culture);

        var response = await _moaApi.GetAgriProductsTransTypeAsync(
            start_time: today,
            end_time: today,
            cropName: cropName
        );

        if (response?.Data == null || !response.Data.Any())
        {
            return new AgriProductsPriceResult
            {
                IsSuccess = false,
                Message = $"今天找不到『{cropName}』的交易行情資料。",
                Suggestion = "可能原因：1. 農產品名稱輸入有誤 2. 該農產品今天沒有交易 3. 該農產品不在開放資料中。請檢查農產品名稱或試試其他農產品。"
            };
        }

        return new AgriProductsPriceResult
        {
            IsSuccess = true,
            CropName = cropName,
            TransDate = response.Data.FirstOrDefault()?.TransDate,
            Products = response.Data.Select(p => new AgriProductPrice
            {
                CropCode = p.CropCode,
                CropName = p.CropName,
                MarketCode = p.MarketCode,
                MarketName = p.MarketName,
                UpperPrice = p.UpperPrice,
                MiddlePrice = p.MiddlePrice,
                LowerPrice = p.LowerPrice,
                AvgPrice = p.AvgPrice,
                TransQuantity = p.TransQuantity
            }).ToList()
        };
    }

    [KernelFunction("get_available_crops")]
    [Description("獲取當前市場有哪些農產品銷售。返回今天市場上所有有交易的農產品名稱和市場名稱")]
    public async Task<AvailableCropsResult?> GetAvailableCropsAsync()
    {
        var culture = new CultureInfo("zh-TW")
        {
            DateTimeFormat =
            {
                Calendar = new TaiwanCalendar()
            }
        };

        var today = DateTime.Now.ToString("yyy.MM.dd", culture);
        
        var response = await _moaApi.GetAgriProductsTransTypeAsync(
            start_time: today,
            end_time: today
        );

        if (response?.Data == null || !response.Data.Any())
        {
            return new AvailableCropsResult
            {
                IsSuccess = false,
                Message = "目前無法獲取今天的農產品交易資料",
                TransDate = today
            };
        }

        var cropGroups = response.Data
            .GroupBy(p => p.CropName)
            .Select(g => new CropInfo
            {
                CropName = g.Key,
                CropCode = g.First().CropCode,
                Markets = g.Select(p => new MarketInfo
                {
                    MarketName = p.MarketName,
                    MarketCode = p.MarketCode,
                    AvgPrice = p.AvgPrice
                }).Distinct().ToList()
            })
            .OrderBy(c => c.CropName)
            .ToList();

        return new AvailableCropsResult
        {
            IsSuccess = true,
            TransDate = today,
            TotalCrops = cropGroups.Count,
            Crops = cropGroups,
            Suggestion = $"今天市場上有 {cropGroups.Count} 種農產品交易。您可以選擇任一農產品來查詢詳細的交易行情。例如：查詢『{cropGroups.FirstOrDefault()?.CropName}』的價格"
        };
    }
}

/// <summary>
/// 農產品價格查詢結果
/// </summary>
public class AgriProductsPriceResult
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public string? CropName { get; set; }

    public string? TransDate { get; set; }

    public List<AgriProductPrice>? Products { get; set; }

    public string? AvailableOptions { get; set; }

    public string? Suggestion { get; set; }
}

/// <summary>
/// 單一農產品價格信息
/// </summary>
public class AgriProductPrice
{
    public string? CropCode { get; set; }

    public string? CropName { get; set; }

    public string? MarketCode { get; set; }

    public string? MarketName { get; set; }

    public decimal? UpperPrice { get; set; }

    public decimal? MiddlePrice { get; set; }

    public decimal? LowerPrice { get; set; }

    public decimal? AvgPrice { get; set; }

    public decimal? TransQuantity { get; set; }
}

/// <summary>
/// 可用農產品列表結果
/// </summary>
public class AvailableCropsResult
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public string? TransDate { get; set; }

    public int TotalCrops { get; set; }

    public List<CropInfo>? Crops { get; set; }

    public string? Suggestion { get; set; }
}

/// <summary>
/// 農產品信息
/// </summary>
public class CropInfo
{
    public string? CropName { get; set; }

    public string? CropCode { get; set; }

    public List<MarketInfo>? Markets { get; set; }
}

/// <summary>
/// 市場信息
/// </summary>
public class MarketInfo
{
    public string? MarketName { get; set; }

    public string? MarketCode { get; set; }

    public decimal? AvgPrice { get; set; }
}
