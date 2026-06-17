using Microsoft.EntityFrameworkCore;
using Gym.Models;
using Gym.Configurations;

namespace Gym.AppDbContext
{
    public class GymDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GymDb;Trusted_Connection=True;trustservercertificate=True;");
        }
        public DbSet<Plan> Plans { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfiguration(new PlanConfiguration());
        }
    }
}
