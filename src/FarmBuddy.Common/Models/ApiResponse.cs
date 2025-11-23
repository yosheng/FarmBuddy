namespace FarmBuddy.Common.Models;

/// <summary>
/// 错误代码枚举
/// </summary>
public enum ErrorCode
{
    None = 0,
    
    /// <summary>
    /// 业务异常（用户输入或业务逻辑错误）
    /// </summary>
    BusinessError = 1000,

    /// <summary>
    /// 验证失败
    /// </summary>
    ValidationError = 1001,

    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized = 1002,

    /// <summary>
    /// 禁止访问
    /// </summary>
    Forbidden = 1003,

    /// <summary>
    /// 资源不存在
    /// </summary>
    NotFound = 1004,

    /// <summary>
    /// 系统错误（未预期的异常）
    /// </summary>
    SystemError = 5000
}

/// <summary>
/// API 响应基类
/// </summary>
public abstract class ApiResponseBase
{
    /// <summary>
    /// 错误码
    /// </summary>
    public ErrorCode Code { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; set; }

    protected ApiResponseBase(ErrorCode code, string message)
    {
        Code = code;
        Message = message;
    }
}

/// <summary>
/// 统一的 API 响应包装类（泛型）
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
public class ApiResponse<T> : ApiResponseBase
{
    /// <summary>
    /// 响应数据
    /// </summary>
    public T? Data { get; set; }

    public ApiResponse(ErrorCode code, string message, T? data = default)
        : base(code, message)
    {
        Data = data;
    }
}

/// <summary>
/// 统一的 API 响应包装类（非泛型，用于错误响应）
/// </summary>
public class ApiResponse : ApiResponse<object?>
{
    public ApiResponse(ErrorCode code, string message)
        : base(code, message, null)
    {
    }
}
