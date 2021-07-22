using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelUWP.Models
{
    public class Reservation
    {
        public String Id { get; set; }



        public string GuestId { get; set; }
        public Guest Guest { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod {get;set;}


         
    }
}
