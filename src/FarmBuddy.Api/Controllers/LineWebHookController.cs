using System.Security.Cryptography;
using System.Text;
using FarmBuddy.Service.Options;
using FarmBuddy.Service.Services;
using LineMessaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FarmBuddy.Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class LineWebHookController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IOptions<LineConfig> _lineConfig;
    private readonly ILogger<LineWebHookController> _logger;

    public LineWebHookController(IChatService chatService, IOptions<LineConfig> lineConfig,
        ILogger<LineWebHookController> logger)
    {
        _chatService = chatService;
        _lineConfig = lineConfig;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        // 允許重複讀取Request body
        Request.EnableBuffering();
        
        _logger.LogInformation($"ClientSecret: {_lineConfig.Value.ChannelSecret}");

        // 驗證請求簽名
        if (!await IsValid())
        {
            return Unauthorized("Invalid signature");
        }

        // 解析Webhook內容
        var content = await GetContent();
        if (content == null)
        {
            return BadRequest("Invalid webhook content");
        }

        // 處理Webhook事件
        await _chatService.ReplyLineChatMessageAsync(content);

        return Ok();
    }

    private async Task<bool> IsValid()
    {
        if (!Request.Headers.TryGetValue("X-Line-Signature", out var signatureValues))
        {
            return false;
        }

        if (string.IsNullOrEmpty(signatureValues))
        {
            return false;
        }

        byte[] body;

        using (var ms = new MemoryStream())
        {
            await Request.Body.CopyToAsync(ms);
            body = ms.ToArray();
        }

        Request.Body.Seek(0, SeekOrigin.Begin);

        // 5. 使用Channel Secret進行HMAC-SHA256驗證
        var secret = Encoding.UTF8.GetBytes(_lineConfig.Value.ChannelSecret);
        using (var hmacsha256 = new HMACSHA256(secret))
        {
            var hash = Convert.ToBase64String(hmacsha256.ComputeHash(body));
            return hash == signatureValues;
        }
    }

    private async Task<LineWebhookContent?> GetContent()
    {
        // 重置stream位置
        if (Request.Body.CanSeek)
        {
            Request.Body.Seek(0, SeekOrigin.Begin);
        }

        // 讀取request body
        using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
        var contentJson = await reader.ReadToEndAsync();
        
        _logger.LogInformation($"LineContent: {contentJson}");
        
        if (string.IsNullOrWhiteSpace(contentJson))
        {
            return null;
        }

        // 反序列化為LineWebhookContent
        var content = JsonConvert.DeserializeObject<LineWebhookContent>(contentJson);
        return content;
    }
}