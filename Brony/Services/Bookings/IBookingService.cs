using Brony.Domain;
using Brony.Models;
using Brony.Models.Bookings;

namespace Brony.Services.Bookings;

public interface IBookingService
{
    void Book(BookingCreateModel createModel);

    void Cancel(int bookingId);

    void ChangeDateTime(int bookingId, DateTime startDate, DateTime endDate);

    BookingViewModel Get(int id);

    List<Booking> GetAll();

    List<BookingViewModel> GetAllByUserId(int userId);

    List<BookingViewModel> GetAllByStadiumId(int stadiumId);
}