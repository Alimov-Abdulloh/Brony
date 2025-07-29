using Brony.Constants;
using Brony.Helpers;
using Newtonsoft.Json;

namespace Brony.Domain;

public class User : BaseEntity
{
    public User()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.UsersFilePath);
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}