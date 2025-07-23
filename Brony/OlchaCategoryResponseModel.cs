using Newtonsoft.Json;

namespace Brony;

public class OlchaCategoryResponseModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name_oz")]
    public string Name { get; set; }
}