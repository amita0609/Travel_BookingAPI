using Microsoft.EntityFrameworkCore;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;

namespace TravelBookingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Airline> Airlines { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Journey> JourneyTable { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Airline>()
        //    //.HasIndex(u => u.AirlineCode)
        //    //          .IsUnique();




        //    modelBuilder.Entity<User>().HasData(

        //                new User()
        //                {
        //                    Id = 1,
        //                    Name = "Jam",
        //                    Email = "jam@gmail.com",
        //                    Password = "custom123",
        //                    Role = "custom"
        //                },

        //                new User()
        //                {
        //                    Id = 2,
        //                    Name = "saim",
        //                    Email = "siam@gmail.com"
        //                    ,
        //                    Password = "normal123",
        //                    Role = "normal"
        //                },

        //                new User()
        //                {
        //                    Id = 3,
        //                    Name = "John",
        //                    Email = "john@gmail.com",
        //                    Password = "johny123",
        //                    Role = "custom"
        //                }


        //        );
        //}
    }
}
