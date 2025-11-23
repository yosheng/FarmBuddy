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
    public async Task<ActionResult<LoginOutputDto>> Login([FromBody] LoginInputDto input)
    {
        var result = await _authService.LoginAsync(input);
        return Ok(result);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenOutputDto>> Refresh([FromBody] RefreshTokenInputDto input)
    {
        var result = await _authService.RefreshTokenAsync(input);
        return Ok(result);
    }
}
