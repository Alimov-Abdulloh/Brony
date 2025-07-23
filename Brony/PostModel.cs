using Newtonsoft.Json;

namespace Brony;

public class PostModel
{
    
    [JsonProperty("userId")]
    public int UserId { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }   

    [JsonProperty("body")]
    public string Body { get; set; }
}