using FarmBuddy.Service.Options;
using LineMessaging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FarmBuddy.Service.Services;

public interface IChatService
{
    Task ReplyLineChatMessageAsync(LineWebhookContent lineWebhookContent);
}

public class ChatService : IChatService
{
    private readonly IKernelManager _kernelManager;
    private readonly LineOAuthClient _oAuthClient;
    private readonly IOptions<LineConfig> _lineConfig;

    public ChatService(IKernelManager kernelManager, IOptions<LineConfig> lineConfig)
    {
        _kernelManager = kernelManager;
        _lineConfig = lineConfig;
        _oAuthClient = new LineOAuthClient(_lineConfig.Value.ChannelId, _lineConfig.Value.ChannelSecret);
    }

    public async Task ReplyLineChatMessageAsync(LineWebhookContent lineWebhookContent)
    {
        if (!lineWebhookContent.Events.Any())
        {
            return;
        }

        var history = new ChatHistory();
        history.AddUserMessage(lineWebhookContent.Events.First().Message.Text);

        var content = await _kernelManager.GetChatMessageContentAsync(history);
        
        var oAuthTokenResponse = await _oAuthClient.GetAccessToken();
        
        var messagingClient = new LineMessagingClient(oAuthTokenResponse.AccessToken);
        await messagingClient.ReplyMessage()
    }
}