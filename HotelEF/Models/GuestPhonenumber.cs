using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelUWP.Models
{
    public class GuestPhonenumber
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
