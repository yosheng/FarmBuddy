using FarmBuddy.Service.Options;
using LineMessaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FarmBuddy.Service.Services;

public interface ILineService
{
    Task ReplyLineChatMessageAsync(LineWebhookContent lineWebhookContent);
}

public class LineService : ILineService
{
    private readonly IKernelManager _kernelManager;
    private readonly LineOAuthClient _oAuthClient;
    private readonly ILogger<LineService> _logger;

    public LineService(IKernelManager kernelManager, IOptions<LineConfig> lineConfig, ILogger<LineService> logger)
    {
        _kernelManager = kernelManager;
        _logger = logger;
        _oAuthClient = new LineOAuthClient(lineConfig.Value.ChannelId, lineConfig.Value.ChannelSecret);
    }

    public async Task ReplyLineChatMessageAsync(LineWebhookContent lineWebhookContent)
    {
        try
        {
            if (lineWebhookContent?.Events == null || !lineWebhookContent.Events.Any())
            {
                _logger.LogWarning("Webhook content is empty or has no events");
                return;
            }

            var firstEvent = lineWebhookContent.Events.First();

            // 只處理文字訊息
            if (firstEvent.Type != WebhookRequestEventType.Message || firstEvent.Message?.Type != MessageType.Text)
            {
                _logger.LogInformation($"Skipping non-text event: {firstEvent.Type}");
                return;
            }

            var userMessage = firstEvent.Message.Text;
            var replyToken = firstEvent.ReplyToken;
            var userId = firstEvent.Source.UserId;

            _logger.LogInformation($"Received user message: {userMessage}, ReplyToken: {replyToken}");

            // 調用AI獲取回應
            var chatContent = await _kernelManager.GetChatMessageContentAsync(userId, userMessage);
            var aiResponse = chatContent.Content;

            _logger.LogInformation($"AI response: {aiResponse}");

            // 使用OAuth取得存取令牌
            var oAuthTokenResponse = await _oAuthClient.GetAccessToken();

            // 準備回應內容
            var messagingClient = new LineMessagingClient(oAuthTokenResponse.AccessToken);

            // 使用ReplyToken回應用戶
            await messagingClient.ReplyMessage(replyToken, aiResponse);

            _logger.LogInformation("Message replied successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error replying to LINE message");
            throw;
        }
    }
}