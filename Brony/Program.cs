using System.Collections;
using System.ComponentModel.Design;
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
using System.Linq;
using Brony.Exceptions;
using Brony.Services.Stadiums;

namespace Brony;

public class Program
{
    static void Main(string[] args)
    {
        var service = new StadiumService(null);

        try
        {
            service.Update(null);
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}