using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Brony.Constants;
using Brony.Domain;
using Brony.Helpers;
using Brony.Models;
using Brony.Models.Users;
using Brony.Services.Users;
using Newtonsoft.Json;

namespace Brony;

public class Program
{
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();

        await Post(httpClient, new PostModel()
        {
            UserId = 2,
            Body = "asdasd",
            Title = "TETS"
        });
        
        await GetAll(httpClient);

    }

    static async Task Get(HttpClient client, int id)
    {
        var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<TestResponseModel>(content);

        Console.WriteLine($"Id: {result.Id} | UserId: {result.UserId} | Title: {result.Title} | Body: {result.Body}");
    }
    
    static async Task GetAll(HttpClient client)
    {
        var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
        
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<TestResponseModel>>(content);

        foreach (var item in result)
        {
            Console.WriteLine($"Id: {item.Id} | UserId: {item.UserId} | Title: {item.Title}\n");
        }
    }

    static async Task Delete(HttpClient client, int id)
    {
        var response = await client.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

        var content = await response.Content.ReadAsStringAsync();
    }

    static async Task Post(HttpClient client, PostModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        await client.PostAsync("https://jsonplaceholder.typicode.com/posts", content);
    }

    static async Task Put(HttpClient client, int id, PostModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        await client.PutAsync($"https://jsonplaceholder.typicode.com/posts/{id}", content);
    }

    static async Task Patch(HttpClient client, int id, PatchModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        await client.PatchAsync($"https://jsonplaceholder.typicode.com/posts/{id}", content);
    }
}
