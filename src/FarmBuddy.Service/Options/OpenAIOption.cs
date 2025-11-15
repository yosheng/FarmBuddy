using System.ComponentModel.DataAnnotations;

namespace FarmBuddy.Service.Options;

public class OpenAIOption
{
    [Required]
    public string ChatModelId { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}