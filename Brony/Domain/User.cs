using Brony.Constants;
using Brony.Helpers;
using Newtonsoft.Json;

namespace Brony.Domain;

public class User
{
    public User()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.UsersFilePath);
    }
    
    public User(int id)
    {
        Id = GeneratorHelper.GenerateId(PathHolder.UsersFilePath);
    }
    
    public int Id { get; set; }
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"{Id},{FirstName},{LastName},{PhoneNumber},{Password}";
    }

    public void Add(int n1, int n2)
    {
        Console.WriteLine($"{n1} + {n2} = {n1 + n2}");
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    private int quantity;

    public void IncreaseQuantity(int quantity)
    {
        this.quantity += quantity;
    }

    public int GetQuantity()
    {
        return quantity;
    }
}