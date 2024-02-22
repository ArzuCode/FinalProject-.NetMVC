using Rent_a_Car_.Net.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Rent_a_Car_.Net.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<About> About { get; set; }



         #region Data Seeding
         protected override void OnModelCreating(ModelBuilder builder)
         {
            base.OnModelCreating(builder);
            
           
        
         }
         #endregion



    }
}