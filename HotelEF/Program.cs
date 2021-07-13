using HotelEF.Data;
using HotelUWP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HotelEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("1. Skapa bokning");
            Console.WriteLine("2. Lista bokningar");
            Console.WriteLine("3. Lista alla rum");
            Console.WriteLine("4. Lista lediga rum");

            var selection = Console.ReadLine();

            if (selection == "1")
            {
                InsertCustomer();
            }
        }

        public static void InsertCustomer()
        {
            
            Guid g = Guid.NewGuid();
            
            Console.Write("Ange Förnamn: ");
            var fname = Console.ReadLine();

            Console.Write("Ange Efternamn: ");
            var lname = Console.ReadLine();

            Console.Write("Ange E-postadress: ");
            var email = Console.ReadLine();

            Console.Write("Ange Telefonnummer: ");
            var phone = Console.ReadLine();

            Console.Write("Ange Alternativt telenr: ");
            var phone2 = Console.ReadLine();

            using (var context = new HotelContext())
            {
                var guest = new Guest
                {
                    Id = g.ToString(),
                    FirstName = fname,
                    LastName = lname,
                    Email = email,

                    GuestPhonenumbers = new List<GuestPhonenumber>
                    {
                    new GuestPhonenumber { GuestId = g.ToString(), PhoneNumber = phone },
                    new GuestPhonenumber { GuestId = g.ToString(), PhoneNumber = phone2 }
                    }
                    
                };

                context.Guests.Add(guest);
                context.SaveChanges();
            }   
        }
    }
}
