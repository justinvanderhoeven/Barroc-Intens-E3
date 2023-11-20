using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using System.Configuration;
using Microsoft.WindowsAppSDK.Runtime.Packages;

namespace BarrocIntens.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractProduct> ContractProducts { get; set; }
        public DbSet<CustomInvoice> CustomInvoices { get; set; }
        public DbSet<CustomInvoiceProduct> CustomInvoiceProducts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MaintenanceAppointment> MaintenanceAppointments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsCategory> ProductsCategories { get; set; }
        public DbSet<User> Users { get; set; }

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
                    Email = "Admin1@gmail.com",
                    Password = SecureHasher.Hash("Admin1"),
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 2,
                    Name = "Justin",
                    Email = "Admin2@gmail.com",
                    Password = SecureHasher.Hash("Admin2"),
                    DepartmentId = 2,
                },
                new User
                {
                    Id = 3,
                    Name = "Remon",
                    Email = "Admin3@gmail.com",
                    Password = SecureHasher.Hash("Admin3"),
                    DepartmentId = 4,
                },
                new User
                {
                    Id = 4,
                    Name = "Dani",
                    Email = "Admin4@gmail.com",
                    Password = SecureHasher.Hash("Admin4"),
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 5,
                    Name = "Nathan",
                    Email = "Admin5@gmail.com",
                    Password = SecureHasher.Hash("Admin5"),
                    DepartmentId = 1,
                },
                new User
                {
                    Id = 6,
                    Name = "User1",
                    Email = "User1@gmail.com",
                    Password = SecureHasher.Hash("User1"),
                    DepartmentId = 6,
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "TestCompany",
                    Phone = "1234",
                    Address = "CompanyStreet",
                    Zipcode = "23",
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
                    Description = "Test",
                    DateAdded = DateTime.Now,
                    ProductId = 1,
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Type = "Customer"
                },
                new Department
                {
                    Id = 2,
                    Type = "Finance"
                },
                new Department
                {
                    Id = 3,
                    Type = "Maintenance"
                },
                new Department
                {
                    Id = 4,
                    Type = "Sales"
                },
                new Department
                {
                    Id = 5,
                    Type = "Purchase"
                },
                new Department
                {
                    Id = 6,
                    Type = "User"
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
                    Name = "Barroc Intens Italian",
                    Description = "Een Koffie machine",
                    ImagePath = "",
                    Price = 499,
                    Stock = 50,
                    ProductsCategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Barroc Intens Italian Deluxe",
                    Description = "Een Koffie machine",
                    ImagePath = "",
                    Price = 499,
                    Stock = 50,
                    ProductsCategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Barroc Intens Italian Deluxe Special",
                    Description = "Een Koffie machine",
                    ImagePath = "",
                    Price = 499,
                    Stock = 50,
                    ProductsCategoryId = 1
                },
                new Product
                {
                    Id = 5,
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
