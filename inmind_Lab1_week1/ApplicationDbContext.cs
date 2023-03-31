using inmind_Lab1_week1.Entities;
using Microsoft.EntityFrameworkCore;

namespace inmind_Lab1_week1;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    {
    }
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=postgres;Password=Idkwhattoput979125!?;");
    }
    public DbSet<Student> Students { get; set; }
}