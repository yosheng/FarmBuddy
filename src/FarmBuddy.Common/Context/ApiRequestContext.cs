namespace FarmBuddy.Common.Context;

/// <summary>
/// API 请求上下文
/// 存储当前请求的用户信息
/// </summary>
public class ApiRequestContext
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? Username { get; set; }
}
