using HotelEF.Data;
using HotelUWP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! ");
            Console.WriteLine("1. Make reservation");
            Console.WriteLine("2. Lista bokningar");
            Console.WriteLine("3. Lista alla rum");
            Console.WriteLine("4. Lista lediga rum");

            var selection = int.Parse(Console.ReadLine());

            if (selection == 1)
            {
               Makereservation();
            }
            if (selection == 2)
            {
                ListReservations();
            }
        }
        public static void ListReservations()
        {
            using (var context = new HotelContext())
            {
                var reservations = context.Reservations.ToList();
                foreach(var item in reservations)
                {
                    Console.WriteLine(item.Id + " " + item.Guest.FirstName);   
                }
            }
        }
        public static void Makereservation()
        {
            
            Guid g = Guid.NewGuid();
            var guestId = g.ToString();
            Console.Write("Guests firstname: ");
            var fname = Console.ReadLine();
            Console.Write("Guests lastname: ");
            var lname = Console.ReadLine();
            Console.Write("Guests email: ");
            var email = Console.ReadLine();
            Console.Write("Guests phonenumber: ");
            var phone = Console.ReadLine();
            Console.Write("Guests alternative phonenumber: ");
            var phone2 = Console.ReadLine();

            using (var context = new HotelContext())
            {
                var guest = new Guest
                {
                    Id = guestId,
                    FirstName = fname,
                    LastName = lname,
                    Email = email,

                    GuestPhonenumbers = new List<GuestPhonenumber>
                    {
                    new GuestPhonenumber { GuestId = guestId, PhoneNumber = phone },
                    new GuestPhonenumber { GuestId = guestId, PhoneNumber = phone2 }
                    }
                    
                };

                context.Guests.Add(guest);
                context.SaveChanges();
                
                Console.WriteLine(" ");
                Console.WriteLine("Select paymentmethod (1-3):");
                foreach (var item in context.PaymentMethods.ToList())
                {
                    Console.WriteLine($"{item.Id} {item.PaymentTypeName}");
                }
                int paymentMethodId = int.Parse(Console.ReadLine());

                Console.WriteLine(" ");
                Console.WriteLine("Check in date  ");
                var checkin = Console.ReadLine();
                Console.WriteLine("Check out date  ");
                var checkout = Console.ReadLine();
                Console.WriteLine(" ");
                Console.WriteLine("Available rooms: ");

                var reservedRooms = context.Rooms.Include(x => x.RoomType).AsNoTracking()
                .Where(c=>c.CheckInDate >= DateTime.Parse(checkin))
                .Where(c => c.CheckOutDate <= DateTime.Parse(checkout))
                .ToList();
                var allRooms = context.Rooms.Include(x => x.RoomType).AsNoTracking().ToList();
                var freeRooms = allRooms.Except(reservedRooms, new ObjectComparer()).ToList();

                foreach (var item in freeRooms)
                {
                    Console.WriteLine(item.Id + " " + item.RoomType.RoomTypeName);
                }
                
                Console.WriteLine("");
                Console.WriteLine("Select room : ");
                var roomId = int.Parse(Console.ReadLine());
                var reservationId = Guid.NewGuid().ToString();
                var roomType = context.Rooms.Where(c => c.Id == roomId).AsNoTracking().FirstOrDefault();
              
                var reserveRoom = new Room
                {
                    Id = roomId,
                    ReservationId = reservationId,
                    RoomTypeId=roomType.RoomTypeId,
                    CheckInDate = DateTime.Parse(checkin),
                    CheckOutDate = DateTime.Parse(checkout)
                };
               
                var reservation = new Reservation
                {
                    Id= reservationId,
                    PaymentMethodId= paymentMethodId,
                    GuestId=guestId
                };
               
                context.Entry(reserveRoom).State = EntityState.Modified;
                context.Reservations.Add(reservation);
                context.SaveChanges();
               
                }
            }   
        }
    public class ObjectComparer : IEqualityComparer<Room>
    {
        public int GetHashCode(Room room)
        {
            if (room == null)
            {
                return 0;
            }
            return room.Id.GetHashCode();
        }

        public bool Equals(Room x1, Room x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }
            return x1.Id == x2.Id;
        }
    }
}

