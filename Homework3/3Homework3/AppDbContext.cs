using Microsoft.EntityFrameworkCore;

namespace _3Homework3;

public class AppDbContext : DbContext
{
    public DbSet<Zoo> Zoos { get; set; }

    public DbSet<Animal> Animals { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=zoo.db");
    }
}