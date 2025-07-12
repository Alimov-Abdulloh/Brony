using Brony.Helpers;
using Brony.Models;
using Brony.Services.Users;

namespace Brony;

public class Program
{
    static void Main(string[] args)
    {
        UserService sevice = new UserService();

        sevice.Get(4);
    }
}
