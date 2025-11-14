using System.Text.Json.Serialization;

namespace FarmBuddy.Service.ThirdApi.Cwa;

public class CwaBaseResponse
{
    public string Success { get; set; }
    public Result Result { get; set; }
}

public class Result
{
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; }
    public Fields[] Fields { get; set; }
}
    
public class Fields
{
    public string Id { get; set; }
    public string Type { get; set; }
}