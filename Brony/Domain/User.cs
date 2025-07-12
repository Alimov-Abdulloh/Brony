using Brony.Constants;
using Brony.Helpers;

namespace Brony.Domain;

public class User
{
    public User()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.UsersFilePath);
    }
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"{Id},{FirstName},{LastName},{PhoneNumber},{Password}";
    }
}