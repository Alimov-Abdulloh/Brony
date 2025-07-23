using Newtonsoft.Json;

namespace Brony;

public class PatchModel
{
    [JsonProperty("title")]
    public string Title { get; set; }   
}