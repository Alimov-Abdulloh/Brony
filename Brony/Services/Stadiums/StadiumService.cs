using Brony.Constants;
using Brony.Domain;
using Brony.Exceptions;
using Brony.Extensions;
using Brony.Helpers;
using Brony.Models;
using Brony.Models.Stadiums;
using Brony.Services.Bookings;

namespace Brony.Services.Stadiums;

public class StadiumService : IStadiumService
{
    private readonly BookingService bookingService;
    public StadiumService(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    public void Create(StadiumCreateModel stadiumCreateModel)
    {
        var convertedStadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        
        var existingStadium = convertedStadiums.Find(x => x.Name == stadiumCreateModel.Name);
        if (existingStadium != null)
            throw new AlreadyExistException($"Stadium with this name <{stadiumCreateModel.Name}> already exists");

        var phoneValidation = stadiumCreateModel.PhoneNumber.ValidatePhone();
        if(!phoneValidation.IsValid)
            throw new ArgumentIsNotValidException(phoneValidation.Error);
        
        convertedStadiums.Add(new Stadium
        {
            Name = stadiumCreateModel.Name,
            PhoneNumber = stadiumCreateModel.PhoneNumber,
            Description = stadiumCreateModel.Description,
            Location = stadiumCreateModel.Location,
            Length = stadiumCreateModel.Length,
            Price = stadiumCreateModel.Price,
            Width = stadiumCreateModel.Width,
            StartWorkingTime = stadiumCreateModel.StartWorkingTime,
            EndWorkingTime = stadiumCreateModel.EndWorkingTime
        });
        
        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, convertedStadiums);    
    }

    public void Update(StadiumUpdateModel model)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        
        var existStadium = stadiums.Find(x => x.Id == model.Id)
            ?? throw new NotFoundException("Stadium is not found");
        
        var alreadyExistStadium = stadiums.Find(x => x.Name == model.Name);
        if (alreadyExistStadium != null)
            throw new AlreadyExistException($"Stadium already exists with this name = {model.Name}");

        var phoneValidation = model.PhoneNumber.ValidatePhone();
        if(!phoneValidation.IsValid)
            throw new ArgumentIsNotValidException(phoneValidation.Error);

        existStadium.Name = model.Name;
        existStadium.Location = model.Location;
        existStadium.Length = model.Length;
        existStadium.Width = model.Width;
        existStadium.PhoneNumber = model.PhoneNumber;
        existStadium.Price = model.Price;
        existStadium.Description = model.Description;

        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, stadiums);
    }

    public void Delete(int id)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        
        var existStadium = stadiums.Find(x => x.Id == id)
            ?? throw new NotFoundException("Stadium is not found");

        stadiums.Remove(existStadium);

        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, stadiums);
    }

    public Stadium Get(int id)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
       
        var existStadium = stadiums.Find(x => x.Id == id)
            ?? throw new NotFoundException("Stadium is not found");

        return existStadium;
    }

    public List<Stadium> GetAll(
        string search,
        decimal? price,
        DateTime? startTime,
        DateTime? endTime)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);

        // Search with given argument
        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            
            stadiums = stadiums.Where(s => 
                s.Name.ToLower().Contains(search) || 
                s.Location.ToLower().Contains(search))
                .ToList();
        }

        // Filter with given argument
        if (price != null)
            stadiums = stadiums.Where(s => s.Price == price).ToList();

        // Filter by start time        
        if (startTime != null)
        {
            var stadiumIds = bookingService.GetAvailableStadiumIdsByStartTime(startTime.Value);
            stadiums = stadiums.Where(s => stadiumIds.Contains(s.Id)).ToList();
        }

        // Filter by end time 
        if (endTime != null)
        {
            var stadiumIds = bookingService.GetAvailableStadiumIdsByEndTime(endTime.Value);
            stadiums = stadiums.Where(s => stadiumIds.Contains(s.Id)).ToList();
        }
        
        return stadiums;
    }
}