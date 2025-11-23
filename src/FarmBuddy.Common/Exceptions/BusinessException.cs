using FarmBuddy.Common.Response;

namespace FarmBuddy.Common.Exceptions;

/// <summary>
/// 业务异常
/// 用于抛出业务逻辑相关的异常，会被 Middleware 捕获并返回给前端
/// </summary>
public class BusinessException : Exception
{
    public ErrorCode Code { get; set; }

    public BusinessException(ErrorCode code, string message) : base(message)
    {
        Code = code;
    }
}
