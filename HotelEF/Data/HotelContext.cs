using HotelUWP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelEF.Data
{
    class HotelContext: DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestPhonenumber> GuestPhoneNumbers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
            .HasOne(a => a.Reservation)
            .WithOne(a => a.Room)
            .HasForeignKey<Room>(c => c.ReservationId);
            
            modelBuilder.Entity<RoomType>().HasData(
                new RoomType
                {
                    Id = 1,
                    RoomTypeName = "Enkelrum",
                    Price =600,
                },
                new RoomType
                {
                    Id = 2,
                    RoomTypeName = "Dubbelrum",
                    Price = 900,
                },
                new RoomType
                {
                    Id = 3,
                    RoomTypeName = "Svit",
                    Price = 1200,
                }
            );
            modelBuilder.Entity<Room>().HasData(
               new Room
               {
                   Id = 1,
                   RoomTypeId = 1
               },
               new Room
               {
                   Id = 2,
                   RoomTypeId=1
               },
               new Room
               {
                   Id = 3,
                   RoomTypeId = 2
               },
               new Room
               {
                   Id = 4,
                   RoomTypeId = 2
               },
               new Room
               {
                   Id = 5,
                   RoomTypeId = 3
               },
               new Room
               {
                   Id = 6,
                   RoomTypeId = 3
               }
           );
            modelBuilder.Entity<PaymentMethod>().HasData(
               new PaymentMethod
               {
                   Id = 1,
                   PaymentTypeName = "Swisch"
               },
               new PaymentMethod
               {
                   Id = 2,
                   PaymentTypeName = "Kort"
               },
               new PaymentMethod
               {
                   Id = 3,
                   PaymentTypeName = "Kontant"
               }
           );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EFHotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
           
        }

    }
}
