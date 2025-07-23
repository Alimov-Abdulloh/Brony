using Newtonsoft.Json;

namespace Brony;

public class OlchaDataResponseModel
{
    [JsonProperty("categories")]
    public List<OlchaCategoryResponseModel> Categories { get; set; }
}