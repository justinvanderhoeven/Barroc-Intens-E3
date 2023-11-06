using Microsoft.EntityFrameworkCore;
using BarrocIntens.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using System.Configuration;

namespace BarrocIntens
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString, 
                ServerVersion.Parse("5.7.33-winx64"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Test",
                    Username = "Bob",
                    Password = "1234"
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "TestCompany",
                    Phone = "1234",
                    Street = "CompanyStreet",
                    HouseNumber = "23",
                    City = "CompanyCity",
                    CountryCode = "34",
                    BkrCheckedAt = DateTime.Now,
                    //UserId = 1,
                }
            );

            modelBuilder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithMany(c => c.Users)
                .UsingEntity<Note>();
        }
    } 
}
