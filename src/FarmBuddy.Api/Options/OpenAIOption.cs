using System.ComponentModel.DataAnnotations;

namespace FarmBuddy.Api.Options;

public class OpenAIOption
{
    [Required]
    public string ChatModelId { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}