using System.Text.Json.Serialization;

namespace FarmBuddy.Service.ThirdApi.Moa.Response;

/// <summary>
/// 農委會API通用回應包裝
/// </summary>
/// <typeparam name="T">回應資料型別</typeparam>
public class MoaResponse<T>
{
    /// <summary>是否有下一頁</summary>
    public bool Next { get; set; }

    /// <summary>回應資料</summary>
    public List<T>? Data { get; set; }
}
