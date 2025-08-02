namespace Brony.Models.Stadiums;

public class StadiumViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public int BeforeCancellationTimeInHours { get; set; }
    public TimeSpan StartWorkingTime { get; set; }
    public TimeSpan EndWorkingTime { get; set; }
}