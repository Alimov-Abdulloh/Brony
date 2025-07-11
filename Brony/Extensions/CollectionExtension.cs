using Brony.Domain;

namespace Brony.Extensions;

public static class CollectionExtension
{
    public static List<string> CovertToString(this List<User> users)
    {
        var convertedUser = new List<string>();
        
        foreach (var user in users)
        {
            convertedUser.Add(user.ToString());
        }

        return convertedUser;
    }
    
    public static List<string> CovertToString(this List<Booking> bookings)
    {
        var convertedBooking = new List<string>();
        
        foreach (var booking in bookings)
        {
            convertedBooking.Add(booking.ToString());
        }

        return convertedBooking;
    }
    
    public static List<string> CovertToString(this List<Stadium> stadiums)
    {
        var convertedStadium = new List<string>();
        
        foreach (var stadium in stadiums)
        {
            convertedStadium.Add(stadium.ToString());
        }

        return convertedStadium;
    }
}