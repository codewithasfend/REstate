using EcommerceSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSampleApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RealEstateApiDb;");
        }

    }
}
