using System.Net.Mime;
using System.Reflection;
using FarmBuddy.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FarmBuddy.Api.Filters;

public class ApiResponseOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // 找出這個 Action 的實際回傳型別（去掉 Task<> / ActionResult<> 包裝）
        var returnType = GetResponseType(context.MethodInfo);
        if (returnType is null)
            return;

        // 只處理 200 / 201 之類的 JSON 回應
        foreach (var (statusCode, response) in operation.Responses.ToList())
        {
            // 只處理 2xx 且有 application/json
            if (!statusCode.StartsWith("2"))
                continue;

            if (!response.Content.TryGetValue(MediaTypeNames.Application.Json, out var mediaType))
                continue;

            // 原本的 schema 是 T，現在要改成 ApiResponse<T>
            var schema = context.SchemaGenerator.GenerateSchema(
                typeof(ApiResponse<>).MakeGenericType(returnType),
                context.SchemaRepository);

            mediaType.Schema = schema;
        }
    }

    private static Type? GetResponseType(MethodInfo methodInfo)
    {
        // 例如：Task<ActionResult<Foo>> / ActionResult<Bar> / Foo
        var type = methodInfo.ReturnType;

        // Task<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
        {
            type = type.GetGenericArguments()[0];
        }

        // ActionResult<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ActionResult<>))
        {
            type = type.GetGenericArguments()[0];
        }

        // 如果是 IActionResult / ActionResult 這種非泛型 → 沒辦法推斷 T，就不處理
        if (typeof(IActionResult).IsAssignableFrom(type) && !type.IsGenericType)
        {
            return null;
        }
        
        if (typeof(FileResult).IsAssignableFrom(type))
        {
            // 檔案，就不要動 swagger schema
            return null;
        }

        return type;
    }
}