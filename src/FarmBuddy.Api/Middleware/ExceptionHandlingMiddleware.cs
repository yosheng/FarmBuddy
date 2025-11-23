using FarmBuddy.Common.Exceptions;
using FarmBuddy.Common.Response;

namespace FarmBuddy.Api.Middleware;

/// <summary>
/// 全局异常处理 Middleware
/// 捕获所有异常并统一返回 400 状态码 + ApiResponse
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status200OK;

        ApiResponse response;

        if (exception is BusinessException businessEx)
        {
            response = new ApiResponse(businessEx.Code, businessEx.Message);
        }
        else
        {
            // 未预期的异常：返回系统错误
            response = new ApiResponse(ErrorCode.SystemError, "系统错误，请稍后重试");
            _logger.LogError(exception, "系统异常: {Message}, 堆棧: {stack}", exception.Message, exception.StackTrace);
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}

/// <summary>
/// Middleware 扩展方法
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
