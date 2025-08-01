using Brony.Constants;
using Brony.Domain;
using Brony.Exceptions;
using Brony.Helpers;
using Brony.Models.Bookings;

namespace Brony.Services.Bookings;

public class BookingService : IBookingService
{
    public void Book(BookingCreateModel createModel)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);

        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
        var existUser = users.Find(u => u.Id == createModel.UserId)
            ?? throw new NotFoundException("User not found");

        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        var existStadium = stadiums.Find(s => s.Id == createModel.StadiumId)
            ?? throw new NotFoundException("Stadium not found");

        if (existStadium.StartWorkingTime > createModel.StartTime.TimeOfDay ||
            existStadium.EndWorkingTime < createModel.EndTime.TimeOfDay)
            throw new ArgumentIsNotValidException(
                $"Stadium does not work in this time period! | " +
                $"Working time: {existStadium.StartWorkingTime} - {existStadium.EndWorkingTime}");

        var res = bookings.Where(b =>
            b.StadiumId == createModel.StadiumId &&
            b.StartTime >= createModel.StartTime &&
            b.EndTime <= createModel.EndTime);

        if (res.Any())
            throw new NotFoundException("In this time, available stadiums are not found");
        
        double numberOfMatchHours = (createModel.EndTime - createModel.StartTime).TotalHours;
        decimal totalPrice = (decimal)numberOfMatchHours * existStadium.Price;

        bookings.Add(new Booking()
        {
            UserId = createModel.UserId,
            StadiumId = createModel.StadiumId,
            StartTime = createModel.StartTime,
            EndTime = createModel.EndTime,
            Price = totalPrice,
        });

        FileHelper.WriteToFile(PathHolder.BookingsFilePath, bookings);
    }

    public void Cancel(int bookingId)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);

        var existBooking = bookings.Find(x => x.Id == bookingId)
            ?? throw new NotFoundException("Booking not found");

        bookings.Remove(existBooking);

        FileHelper.WriteToFile(PathHolder.BookingsFilePath, bookings);
    }

    public void ChangeDateTime(int bookingId, DateTime startTime, DateTime endTime)
    {
        string text = FileHelper.ReadFromFile(PathHolder.BookingsFilePath);

        List<Booking> convertedBookings = text.ToBooking();

        var existBooking = convertedBookings.Find(x => x.Id == bookingId);

        if (existBooking == null)
        {
            throw new Exception("Booking was not found");
        }

        var existStadium = stadiumService.Get(existBooking.StadiumId);

        // stadium working hours
        TimeSpan startTimeOfStadium = TimeSpan.Parse(existStadium.StartWorkingTime);
        TimeSpan endTimeOfStadium = TimeSpan.Parse(existStadium.EndWorkingTime);

        //booking start and end time
        TimeSpan startTimeOfMatch = startTime.TimeOfDay;
        TimeSpan endTimeOfMatch = endTime.TimeOfDay;


        if (startTimeOfStadium > startTimeOfMatch || endTimeOfStadium < endTimeOfMatch)
        {
            throw new Exception($"Stadium does not work in this time period! | " +
                                $"Working time: {existStadium.StartWorkingTime} - {existStadium.EndWorkingTime}");
        }

        // check time which is not booked
        foreach (var booking in convertedBookings)
        {
            if (booking.StadiumId == existBooking.StadiumId)
            {
                if (booking.StartTime < endTime && booking.EndTime > startTime)
                {
                    throw new Exception("This stadium is already booked in this time");
                }
            }
        }

        existBooking.StartTime = startTime;
        existBooking.EndTime = endTime;

        File.WriteAllLines(PathHolder.BookingsFilePath, convertedBookings.ConvertToString());
    }

    public BookingViewModel Get(int id)
    {
        string text = FileHelper.ReadFromFile(PathHolder.BookingsFilePath);

        List<Booking> convertedBookings = text.ToBooking();

        var existBooking = convertedBookings.Find(x => x.Id == id);

        if (existBooking == null)
        {
            throw new Exception("Booking is not found");
        };

        return existBooking.ToBookingViewModel();
    }

    public List<Booking> GetAll()
    {
        string text = FileHelper.ReadFromFile(PathHolder.BookingsFilePath);

        List<Booking> convertedBookings = text.ToBooking();

        return convertedBookings;
    }

    public List<BookingViewModel> GetAllByUserId(int userId)
    {
        string text = FileHelper.ReadFromFile(PathHolder.BookingsFilePath);

        List<Booking> convertedBookings = text.ToBooking();

        var result = new List<BookingViewModel>();

        foreach (var booking in convertedBookings)
        {
            if (booking.UserId == userId)
            {
                result.Add(booking.ToBookingViewModel());
            }
        }

        return result;
    }

    public List<BookingViewModel> GetAllByStadiumId(int stadiumId)
    {
        string text = FileHelper.ReadFromFile(PathHolder.BookingsFilePath);

        List<Booking> convertedBookings = text.ToBooking();

        var result = new List<BookingViewModel>();

        foreach (var item in convertedBookings)
        {
            if (item.StadiumId == stadiumId)
            {
                result.Add(item.ToBookingViewModel());
            }
        }

        return result;
    }

    public List<int> GetAvailableStadiumIdsByStartTime(DateTime dateTime)
    {
        throw new NotImplementedException();
    }
    
    public List<int> GetAvailableStadiumIdsByEndTime(DateTime dateTime)
    {
        throw new NotImplementedException();
    }
}


