using Brony.Constants;
using Brony.Helpers;

namespace Brony.Domain;

public class Stadium : BaseEntity
{
    public Stadium()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.StadiumsFilePath);
    }
    
    public string Name { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public int BeforeCancellationTime { get; set; }
    public TimeSpan StartWorkingTime { get; set; }
    public TimeSpan EndWorkingTime { get; set; }
}