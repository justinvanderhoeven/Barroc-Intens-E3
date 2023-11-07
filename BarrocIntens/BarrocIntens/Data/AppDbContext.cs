using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using System.Configuration;

namespace BarrocIntens.Data
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Go to the App.config.example file and then follow Instructions

            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString,
                ServerVersion.Parse("5.7.33-winx64"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Companies)
               .WithMany(c => c.Users)
               .UsingEntity<Note>();

            modelBuilder.Entity<Contract>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Contracts)
                .UsingEntity<ContractProduct>();

            modelBuilder.Entity<CustomInvoice>()
                .HasMany(c => c.Products)
                .WithMany(p => p.CustomInvoices)
                .UsingEntity<CustomInvoiceProduct>();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin1",
                    Username = "Admin1",
                    Password = "Admin1",
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 2,
                    Name = "Justin",
                    Username = "Admin2",
                    Password = "Admin2",
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 3,
                    Name = "Remon",
                    Username = "Admin3",
                    Password = "Admin3",
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 4,
                    Name = "Dani",
                    Username = "Admin4",
                    Password = "Admin4",
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 5,
                    Name = "Nathan",
                    Username = "Admin5",
                    Password = "Admin5",
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 6,
                    Name = "User1",
                    Username = "User1",
                    Password = "User1",
                    DepartmentId = 2,
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
                    ContactId = 1,
                }
            );

            modelBuilder.Entity<MaintenanceAppointment>().HasData(
                new MaintenanceAppointment
                {
                    Id = 1,
                    Remark = "Test",
                    DateAdded = DateTime.Now,
                    CompanyId = 1,
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Type = "Admins"
                },
                new Department
                {
                    Id = 2,
                    Type = "Users"
                }
            );

            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id = 1,
                    Content = "Test",
                    Date = DateTime.Now,
                    CompanyId = 1,
                    UserId = 1,
                }
            );

            modelBuilder.Entity<ProductsCategory>().HasData(
                new ProductsCategory
                {
                    Id = 1,
                    Name = "Automaten",
                    IsEmployeeOnly = 1,
                },
                new ProductsCategory
                {
                    Id = 2,
                    Name = "Koffiebonen",
                    IsEmployeeOnly = 1,
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Barroc Intens Italian Light",
                    Description = "Een Koffie machine",
                    ImagePath = "",
                    Price = 499,
                    Stock = 50,
                    ProductsCategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Espresso Beneficio",
                    Description = "Een toegankelijke en zachte koffie. Hij is afkomstig van de Finca El Limoncillo, een weelderige plantage aan de rand van het regenwoud in de Matagalpa regio in Nicaragua",
                    ImagePath = "",
                    Price = 21,
                    Stock = 50,
                    ProductsCategoryId = 2
                }
            );

            modelBuilder.Entity<Contract>().HasData(
                new Contract
                {
                    Id = 1,
                    CompanyId = 1,
                    ActiveUntil = DateOnly.FromDateTime( DateTime.UtcNow ),
                }
            );

            modelBuilder.Entity<ContractProduct>().HasData(
                new ContractProduct
                {
                    Id = 1,
                    ContractId = 1,
                    ProductId = 1,
                    Amount = 5,
                    PricePerProduct = 5,
                }
            );

            modelBuilder.Entity<CustomInvoice>().HasData(
               new CustomInvoice
               {
                   Id = 1,
                   Date = DateTime.Now,
                   PaidAt = DateTime.Now,
                   CompanyId = 1,
               }
           );

            modelBuilder.Entity<CustomInvoiceProduct>().HasData(
                new CustomInvoiceProduct
                {
                    Id = 1,
                    ProductId = 1,
                    CustomInvoiceId = 1,
                    Amount = 50,
                    PricePerProduct = 5,
                }
            );

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = 1,
                    Date = DateOnly.FromDateTime( DateTime.UtcNow ),
                    PaidAt = DateOnly.FromDateTime( DateTime.UtcNow ),
                    ContractId = 1,
                }
            );
        }
    }
}
