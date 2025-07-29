using Brony.Constants;
using Brony.Helpers;

namespace Brony.Domain;

public class Booking : BaseEntity
{
    public Booking()
    {
        Id = GeneratorHelper.GenerateId(PathHolder.BookingsFilePath);
    }
    
    public int UserId { get; set; }
    public int StadiumId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }
}