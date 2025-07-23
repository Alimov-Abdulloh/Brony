using Newtonsoft.Json;

namespace Brony;

public class TestResponseModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("userId")]
    public int UserId { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }   

    [JsonProperty("body")]
    public string Body { get; set; }
}