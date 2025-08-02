using Brony.Domain;
using Brony.Models.Bookings;
using Brony.Models.Stadiums;
using Brony.Models.Users;

namespace Brony.Extensions;

public static class MapExtensions
{
    #region Staduim

    public static Stadium Map(this StadiumCreateModel stadiumCreateModel)
    {
        return new Stadium
        {
            Name = stadiumCreateModel.Name,
            PhoneNumber = stadiumCreateModel.PhoneNumber,
            Description = stadiumCreateModel.Description,
            Location = stadiumCreateModel.Location,
            Length = stadiumCreateModel.Length,
            Price = stadiumCreateModel.Price,
            Width = stadiumCreateModel.Width,
            StartWorkingTime = stadiumCreateModel.StartWorkingTime,
            EndWorkingTime = stadiumCreateModel.EndWorkingTime,
            BeforeCancellationTimeInHours = stadiumCreateModel.BeforeCancellationTimeInHours,
        };
    }
    
    public static void Map(this Stadium oldModel, StadiumUpdateModel newModel)
    {
        oldModel.Name = newModel.Name;
        oldModel.Location = newModel.Location;
        oldModel.Length = newModel.Length;
        oldModel.Width = newModel.Width;
        oldModel.PhoneNumber = newModel.PhoneNumber;
        oldModel.Price = newModel.Price;
        oldModel.Description = newModel.Description;
        oldModel.StartWorkingTime = newModel.StartWorkingTime;
        oldModel.EndWorkingTime = newModel.EndWorkingTime;
        oldModel.BeforeCancellationTimeInHours = newModel.BeforeCancellationTimeInHours;
    }

    public static StadiumViewModel Map(this Stadium stadium)
    {
        return new StadiumViewModel
        {
            Id = stadium.Id,
            Name = stadium.Name,
            PhoneNumber = stadium.PhoneNumber,
            Description = stadium.Description,
            Location = stadium.Location,
            Length = stadium.Length,
            Price = stadium.Price,
            Width = stadium.Width,
            StartWorkingTime = stadium.StartWorkingTime,
            EndWorkingTime = stadium.EndWorkingTime,
            BeforeCancellationTimeInHours = stadium.BeforeCancellationTimeInHours,
        };
    }

    #endregion

    #region User

    public static UserViewModel Map(this User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };
    }

    public static void Map(this User user, UserUpdateModel model)
    {
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;
    }

    #endregion
  
    #region Booking

    public static Booking Map(this BookingCreateModel model)
    {
        return new Booking
        {
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            StadiumId = model.StadiumId,
            UserId = model.UserId,
        };
    }

    #endregion
}