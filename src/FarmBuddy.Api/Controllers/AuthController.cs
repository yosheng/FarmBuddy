using FarmBuddy.Service.Dtos;
using FarmBuddy.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 使用帳號密碼登入
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginOutputDto> Login([FromBody] LoginInputDto input)
    {
        return await _authService.LoginAsync(input);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<RefreshTokenOutputDto> Refresh([FromBody] RefreshTokenInputDto input)
    {
        return await _authService.RefreshTokenAsync(input);
    }
    
    /// <summary>
    /// 取得當前登入的使用者資訊
    /// </summary>
    [HttpGet("me")]
    public async Task<BackendAccountDto> GetMe()
    {
        return await _authService.GetMeAsync();
    }
}
