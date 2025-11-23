using FarmBuddy.Common;
using FarmBuddy.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FarmBuddy.Api.Filters;

/// <summary>
/// API 响应包装过滤器
/// 自动将 Controller 返回的数据包装成 ApiResponse<T>
/// 对于文件响应不进行包装
/// </summary>
public class ApiResponseWrapperFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // 只处理 ObjectResult 且不是文件相关的结果
        if (context.Result is ObjectResult objectResult &&
            !(context.Result is FileResult))
        {
            // 如果已经是 ApiResponseBase，不再包装
            if (objectResult.Value is ApiResponseBase)
            {
                await next();
                return;
            }

            // 包装成功响应
            var wrappedResponse = new ApiResponse<object?>(
                ErrorCode.None,
                "操作成功",
                objectResult.Value);

            context.Result = new OkObjectResult(wrappedResponse);
        }

        await next();
    }
}
