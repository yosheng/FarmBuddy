using FarmBuddy.Service.Dtos;
using FarmBuddy.Service.Services;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<BackendAccountDto>>> GetAll()
    {
        var accounts = await _service.GetAllAsync();
        return Ok(accounts);
    }

    /// <summary>
    /// 根據ID獲取後端帳戶
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BackendAccountDto>> GetById(int id)
    {
        var account = await _service.GetByIdAsync(id);
        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    /// <summary>
    /// 建立新的後端帳戶
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BackendAccountDto>> Create([FromBody] CreateBackendAccountInputDto input)
    {
        var account = await _service.CreateAsync(input);
        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    /// <summary>
    /// 更新後端帳戶
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<BackendAccountDto>> Update(int id, [FromBody] UpdateBackendAccountInputDto input)
    {
        var account = await _service.UpdateAsync(id, input);
        return Ok(account);
    }

    /// <summary>
    /// 刪除後端帳戶
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
