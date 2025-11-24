using FarmBuddy.Common.Models;
using FarmBuddy.Service.Dtos;
using FarmBuddy.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatMessageController : ControllerBase
{
    private readonly IChatMessageService _service;

    public ChatMessageController(IChatMessageService service)
    {
        _service = service;
    }

    /// <summary>
    /// 獲取聊天消息分頁列表
    /// </summary>
    [HttpGet]
    public async Task<PagingResult<ChatMessageDto>> GetPaging([FromQuery] QueryChatMessageDto input)
    {
        return await _service.GetPagingAsync(input);
    }
}
