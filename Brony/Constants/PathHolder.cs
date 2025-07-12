namespace Brony.Constants;

public class PathHolder
{
    private static readonly string parentRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    public static readonly string UsersFilePath = Path.Combine(parentRoot, "Data", "users.txt");
    public static readonly string BookingsFilePath = Path.Combine(parentRoot, "Data", "bookings.txt");
    public static readonly string StadiumsFilePath = Path.Combine(parentRoot, "Data", "stadiums.txt");
}