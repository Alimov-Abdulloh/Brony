using Brony.Constants;
using Brony.Domain;
using Brony.Exceptions;
using Brony.Extensions;
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

        var bookedBookings = bookings.Where(b =>
            b.StadiumId == createModel.StadiumId &&
            b.StartTime >= createModel.StartTime &&
            b.EndTime <= createModel.EndTime);

        if (bookedBookings.Any())
            throw new NotFoundException("This stadium is already booked at this time!");
        
        double numberOfMatchHours = (createModel.EndTime - createModel.StartTime).TotalHours;
        decimal totalPrice = (decimal)numberOfMatchHours * existStadium.Price;

        var mappedModel = createModel.Map();
        
        mappedModel.Price = totalPrice;
        
        bookings.Add(mappedModel);

        FileHelper.WriteToFile(PathHolder.BookingsFilePath, bookings);
    }

    public void Cancel(int bookingId)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);

        var existBooking = bookings.Find(x => x.Id == bookingId)
            ?? throw new NotFoundException("Booking not found");

        var staduim = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath)
            .Find(s => s.Id == existBooking.StadiumId)
            ?? throw new NotFoundException("Stadium not found");

        var cancellationDateTime = existBooking.StartTime.AddHours(-staduim.BeforeCancellationTimeInHours);

        if (DateTime.Now > cancellationDateTime)
            throw new ArgumentIsNotValidException($"Now you can't cancel booking. You may cancel before {cancellationDateTime}");
        
        bookings.Remove(existBooking);

        FileHelper.WriteToFile(PathHolder.BookingsFilePath, bookings);
    }

    public void ChangeDateTime(int bookingId, DateTime startTime, DateTime endTime)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);

        var existBooking = bookings.Find(x => x.Id == bookingId)
            ?? throw new NotFoundException("Booking not found");

        var staduim = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath)
            .Find(s => s.Id == existBooking.StadiumId) ?? throw new NotFoundException("Stadium not found");

        if (staduim.StartWorkingTime > startTime.TimeOfDay ||
            staduim.EndWorkingTime < endTime.TimeOfDay)
            throw new ArgumentIsNotValidException(
                $"Stadium does not work in this time period! | " +
                $"Working time: {staduim.StartWorkingTime} - {staduim.EndWorkingTime}");
        
        var cancellationDateTime = existBooking.StartTime.AddHours(-staduim.BeforeCancellationTimeInHours);
        if (DateTime.Now > cancellationDateTime)
            throw new ArgumentIsNotValidException($"Can't change time. You may change time before {cancellationDateTime}");
        
        var bookedBookings = bookings.Where(b =>
            b.StadiumId == existBooking.StadiumId &&
            b.StartTime >= startTime &&
            b.EndTime <= endTime);

        if (bookedBookings.Any())
            throw new NotFoundException("This stadium is already booked at this time!");
        
        existBooking.EndTime = endTime;
        existBooking.StartTime = startTime;

        FileHelper.WriteToFile(PathHolder.BookingsFilePath, bookings);
    }

    public BookingViewModel Get(int id)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        
        var booking = bookings.Join(
            users,
            booking => booking.UserId,
            user => user.Id,
            (booking, user) => new { booking, user })
            .Join(
                stadiums, 
                temp => temp.booking.StadiumId,
                stadium => stadium.Id,
                (temp, stadium) => new BookingViewModel
                {
                    Id = temp.booking.Id,
                    UserFirstName = temp.user.FirstName,
                    UserLastName = temp.user.LastName,
                    UserPhone = temp.user.PhoneNumber,
                    Price = temp.booking.Price,
                    StartTime = temp.booking.StartTime,
                    EndTime = temp.booking.EndTime,
                    StadiumName = stadium.Name,
                })
            .FirstOrDefault(b => b.Id == id);

        return booking;
    }

    public List<BookingViewModel> GetAll()
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);

        var result = bookings.Join(
                users,
                booking => booking.UserId,
                user => user.Id,
                (booking, user) => new { booking, user })
            .Join(
                stadiums,
                temp => temp.booking.StadiumId,
                stadium => stadium.Id,
                (temp, stadium) => new BookingViewModel
                {
                    Id = temp.booking.Id,
                    UserFirstName = temp.user.FirstName,
                    UserLastName = temp.user.LastName,
                    UserPhone = temp.user.PhoneNumber,
                    Price = temp.booking.Price,
                    StartTime = temp.booking.StartTime,
                    EndTime = temp.booking.EndTime,
                    StadiumName = stadium.Name,
                });

        return result.ToList();
    }

    public List<BookingViewModel> GetAllByUserId(int userId)
    {
        var user = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath)
            .Find(u => u.Id == userId)
            ?? throw new NotFoundException("User not found");
       
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath)
            .Where(b => b.UserId == userId)
            .ToList();
        
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);

        var result = bookings.Join(
            stadiums,
            booking => booking.StadiumId,
            stadium => stadium.Id,
            (booking, stadium) => new BookingViewModel
            {
                Id = booking.Id,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Price = booking.Price,
                StadiumName = stadium.Name,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserPhone = user.PhoneNumber,
            })
            .ToList();
        
        return result;
    }

    public List<BookingViewModel> GetAllByStadiumId(int stadiumId)
    {
        var stadium = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath)
            .Find(s => s.Id == stadiumId)
            ?? throw new NotFoundException("Stadium not found");
        
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath)
            .Where(b => b.StadiumId == stadiumId)
            .ToList();

        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);

        var result = bookings.Join(
            users,
            booking => booking.UserId,
            user => user.Id,
            (booking, user) => new BookingViewModel
            {
                Id = booking.Id,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserPhone = user.PhoneNumber,
                Price = booking.Price,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                StadiumName = stadium.Name,
            })
            .ToList();
        
        return result;
    }

    public List<int> GetAvailableStadiumIdsByStartTime(DateTime dateTime)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);
        
        var stadiumIds = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath)
            .Select(s => s.Id);

        var bookedBookings = bookings.Where(b =>
                stadiumIds.Contains(b.StadiumId) &&
                b.StartTime >= dateTime)
            .Select(b => b.StadiumId);

        var result = stadiumIds.Except(bookedBookings).ToList();
        
        return result;
    }
    
    public List<int> GetAvailableStadiumIdsByEndTime(DateTime dateTime)
    {
        var bookings = FileHelper.ReadFromFile<Booking>(PathHolder.BookingsFilePath);
        
        var stadiumIds = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath)
            .Select(s => s.Id);

        var bookedBookings = bookings.Where(b =>
                stadiumIds.Contains(b.StadiumId) &&
                b.StartTime <= dateTime)
            .Select(b => b.StadiumId);

        var result = stadiumIds.Except(bookedBookings).ToList();
        
        return result;
    }
}


