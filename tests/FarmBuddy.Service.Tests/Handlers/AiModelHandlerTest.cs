using FarmBuddy.Service.Handlers;
using FarmBuddy.Service.Options;
using Microsoft.Extensions.Options;

namespace FarmBuddy.Service.Tests.Handlers;

public class AiModelHandlerTest
{
    private readonly IAiModelHandler _aiModelHandler;
    private readonly IOptions<KernelConfig> _options;

    public AiModelHandlerTest(IAiModelHandler aiModelHandler, IOptions<KernelConfig> options)
    {
        _aiModelHandler = aiModelHandler;
        _options = options;
    }

    [Fact]
    public void Test_Calculate_Tokens()
    {
        var data = _aiModelHandler.CalculateTokens(_options.Value.SystemMessage);
        Assert.True(data > 0);
    }
}