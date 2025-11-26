using FarmBuddy.Common.Models;
using FarmBuddy.Service.Dtos;
using FarmBuddy.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SystemSettingController : ControllerBase
{
    private readonly ISystemSettingService _service;

    public SystemSettingController(ISystemSettingService service)
    {
        _service = service;
    }

    /// <summary>
    /// 獲取所有系統設定
    /// </summary>
    [HttpGet]
    public async Task<PagingResult<SystemSettingDto>> GetPaging([FromQuery] QuerySystemSettingDto input)
    {
        return await _service.GetPagingAsync(input);
    }

    /// <summary>
    /// 更新系統設定
    /// </summary>
    [HttpPut("{id}")]
    public async Task<SystemSettingDto> Update(int id, [FromBody] UpdateSystemSettingInputDto input)
    {
        return await _service.UpdateAsync(id, input);
    }
}
