using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarRentalContext : DbSetContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=CarRental;Trusted_Connection=true");
            }
            catch (Exception)
            {
                var message = "Bir şeyler ters gitti!";
                throw new Exception(message);
            }
        }
    }
}