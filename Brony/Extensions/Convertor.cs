using System.Diagnostics;
using Brony.Constants;
using Brony.Domain;
using Brony.Helpers;
using Brony.Models;
using Brony.Models.Bookings;
using Brony.Services.Stadiums;

namespace Brony.Extensions;

public static class Convertor
{
    public static List<Booking> ToBooking(this string text)
    {
        List<Booking> bookings = new List<Booking>();

        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            bookings.Add(new Booking
            {
                Id = int.Parse(parts[0]),
                UserId = int.Parse(parts[1]),
                StadiumId = int.Parse(parts[2]),
                StartTime = DateTime.Parse(parts[3]),
                EndTime = DateTime.Parse(parts[4]),
                Price = decimal.Parse(parts[5])
            });
        }

        return bookings;
    }

    public static List<Stadium> ToStadium(this string text)
    {
        List<Stadium> stadiums = new List<Stadium>();

        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            stadiums.Add(new Stadium
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Width = float.Parse(parts[2]),
                Length = float.Parse(parts[3]),
                Price = decimal.Parse(parts[4]),
                Location = parts[5],
                PhoneNumber = parts[6],
                Description = parts[7],
                StartWorkingTime = parts[8],
                EndWorkingTime = parts[9]
            });
        }

        return stadiums;
    }

    public static List<User> ToUser(this string text)
    {
        List<User> users = new();

        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            users.Add(new User
            {
                Id = int.Parse(parts[0]),
                FirstName = parts[1],
                LastName = parts[2],
                PhoneNumber = parts[3],
                Password = parts[4],
            });
        }

        return users;
    }

    public static BookingViewModel ToBookingViewModel(this Booking booking)
    {
        // process of getting stadium name
        string stadiumsInTextFormat = FileHelper.ReadFromFile(PathHolder.StadiumsFilePath);

        List<Stadium> convertedStadiums = stadiumsInTextFormat.ToStadium();

        Stadium stadium = convertedStadiums.Find(x => x.Id == booking.StadiumId);

        if (stadium == null)
        {
            throw new ArgumentException("Stadium was not found!");
        }

        // proccess of getting user name
        string usersInTextFormat = FileHelper.ReadFromFile(PathHolder.UsersFilePath);

        List<User> convertedUsers = usersInTextFormat.ToUser();

        User user = convertedUsers.Find(x => x.Id == booking.UserId);

        if (user == null)
        {
            throw new ArgumentException("User was not found!");
        }

        // converting booking domain for view model
        return new BookingViewModel
        {
            User = user.FirstName + " " + user.LastName,
            StadiumName = stadium.Name,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Price = booking.Price,
        };
    }
}

