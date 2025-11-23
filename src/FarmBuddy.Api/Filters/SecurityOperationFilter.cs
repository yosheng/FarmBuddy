using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FarmBuddy.Api.Filters;

public class SecurityOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (actionDescriptor == null)
            return;

        // 1. 從 FilterDescriptors 判斷是否有 AllowAnonymousFilter
        var filterDescriptors = actionDescriptor.FilterDescriptors;

        var hasAllowAnonymousFilter = filterDescriptors
            .Any(f => f.Filter is IAllowAnonymousFilter);

        // 也可以額外用 Attribute 判斷一次（兩種都抓）
        var methodInfo = actionDescriptor.MethodInfo;
        var hasAllowAnonymousAttribute =
            methodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() ||
            methodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() == true;

        var hasAllowAnonymous = hasAllowAnonymousFilter || hasAllowAnonymousAttribute;

        if (hasAllowAnonymous)
        {
            // 有 AllowAnonymous → 不加任何 security requirement → Swagger 不顯示鎖頭
            operation.Security?.Clear();
            return;
        }

        // 2. 這裡假設你用全域 AuthorizeFilter(policy)，只要「沒有 AllowAnonymous」就需要鎖頭
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer" // 必須和 AddSecurityDefinition 的名稱一致
            }
        };

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [scheme] = Array.Empty<string>()
        });
    }
}