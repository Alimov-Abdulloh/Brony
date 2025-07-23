using Newtonsoft.Json;

namespace Brony;

public class OlchaResponseModel
{
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("data")]
    public OlchaDataResponseModel Data { get; set; }
}