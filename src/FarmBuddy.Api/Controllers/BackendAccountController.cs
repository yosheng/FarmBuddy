using FarmBuddy.Common.Models;
using FarmBuddy.Service.Dtos;
using FarmBuddy.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackendAccountController : ControllerBase
{
    private readonly IBackendAccountService _service;

    public BackendAccountController(IBackendAccountService service)
    {
        _service = service;
    }

    /// <summary>
    /// 獲取所有後端帳戶
    /// </summary>
    [HttpGet]
    public async Task<PagingResult<BackendAccountDto>> GetPaging([FromQuery] QueryBackendAccountDto input)
    {
        return await _service.GetPagingAsync(input);
    }

    /// <summary>
    /// 根據ID獲取後端帳戶
    /// </summary>
    [HttpGet("{id}")]
    public async Task<BackendAccountDto?> GetById(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    /// <summary>
    /// 建立新的後端帳戶
    /// </summary>
    [HttpPost]
    public async Task<BackendAccountDto> Create([FromBody] CreateBackendAccountInputDto input)
    {
        return await _service.CreateAsync(input);
    }

    /// <summary>
    /// 更新後端帳戶
    /// </summary>
    [HttpPut("{id}")]
    public async Task<BackendAccountDto> Update(int id, [FromBody] UpdateBackendAccountInputDto input)
    {
        return await _service.UpdateAsync(id, input);
    }

    /// <summary>
    /// 刪除後端帳戶
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}