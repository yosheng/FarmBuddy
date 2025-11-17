using FarmBuddy.Common.Enums;

namespace FarmBuddy.Service.Options;

public class KernelConfig
{
    public AiModelType AiModelType { get; set; }

    public required string SystemMessage { get; set; }

    public required string AssistantMessage { get; set; }
}