using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using FarmBuddy.Common.Context;

namespace FarmBuddy.Api.Middleware;

/// <summary>
/// JWT Token 解析 Middleware
/// 从 Authorization Header 中提取 Bearer Token，并解析 JWT 中的用户信息
/// 将解析结果存储到 ApiRequestContext 中
/// </summary>
public class JwtTokenParsingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtTokenParsingMiddleware> _logger;

    public JwtTokenParsingMiddleware(RequestDelegate next, ILogger<JwtTokenParsingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ApiRequestContext requestContext)
    {
        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader) &&
            AuthenticationHeaderValue.TryParse(authHeader, out var headerValue) &&
            headerValue.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            var token = headerValue.Parameter;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    // 从 JWT Claims 中提取用户信息
                    requestContext.UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    requestContext.Username = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                    _logger.LogInformation("JWT Token 解析成功: UserId={UserId}, Username={Username}",
                        requestContext.UserId, requestContext.Username);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "JWT Token 解析失败");
            }
        }

        await _next(context);
    }
}

/// <summary>
/// Middleware 扩展方法
/// </summary>
public static class JwtTokenParsingMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtTokenParsing(this IApplicationBuilder app)
    {
        return app.UseMiddleware<JwtTokenParsingMiddleware>();
    }
}
