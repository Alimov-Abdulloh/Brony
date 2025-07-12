using Brony.Constants;
using Brony.Helpers;

namespace Brony.Domain;

public class Booking
{
    public Booking()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.BookingsFilePath);
    }
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int StadiumId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{Id},{UserId},{StadiumId},{StartTime},{EndTime},{Price}";
    }
}