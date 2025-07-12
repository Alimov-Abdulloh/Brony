using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brony.Models.Bookings
{
    public class BookingViewModel
    {
        public string User { get; set; }
        public string StadiumName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
    }
}
