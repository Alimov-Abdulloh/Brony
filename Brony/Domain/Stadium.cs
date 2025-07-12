using Brony.Constants;
using Brony.Helpers;

namespace Brony.Domain;

public class Stadium
{
    public Stadium()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.StadiumsFilePath);
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string StartWorkingTime { get; set; }
    public string EndWorkingTime { get; set; }

    public override string ToString()
    {
        return $"{Id}," +
               $"{Name}," +
               $"{Width}," +
               $"{Length}," +
               $"{Price}," +
               $"{Location}," +
               $"{PhoneNumber}," +
               $"{Description}," +
               $"{StartWorkingTime}," +
               $"{EndWorkingTime}";
    }
}