using Azure.Identity;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesUnitTests.MockData
{
    public static class DbContextData
    {
        public static List<Customer> GetCustomers() 
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = "1d084be5-5915-40fa-8809-aa6f5f0c9270",
                    UserName = "twindwa@gmail.com",
                    NormalizedUserName = "TWINDWA@GMAIL.COM",
                    Email = "twindwa@gmail.com",
                    NormalizedEmail = "TWINDWA@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEBY+zFCJjwBgcJWJsqBcoyXzpoYycCUh+fk5UPAsTZ1e9th5XcH9E7sWBs5KAa3ikA==",
                    SecurityStamp = "MZYHN6YKNLMARQ4U5M4PMEFUFVVEFUXS",
                    ConcurrencyStamp = "b2bbf6c6-e878-4044-b035-e74ffa6754e2",
                    PhoneNumber = "380977370031",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 19,
                    Price = 20.99M,
                    Name = "I5 8400",
                    Description = "best processor 2018",
                    ProductTypeId = 1,
                    IsVisible = true,
                    Quantity = 3
                }
            };
        }
    }
}
