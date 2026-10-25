
using FoodLoop.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodLoop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
    }
}
