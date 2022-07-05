using EcommerceSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSampleApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(@"Server=(localdb)\MSSQLLocalDB;Database=RealEstateInMemoryDb;");
        }

    }
}
