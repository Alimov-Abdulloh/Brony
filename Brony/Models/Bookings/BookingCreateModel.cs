using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brony.Models.Bookings
{
    public class BookingCreateModel
    {
        public int UserId { get; set; }
        public int StadiumId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
