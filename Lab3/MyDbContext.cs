using Lab3.Entities;
using Microsoft.EntityFrameworkCore;
namespace Lab3;

public class MyDbContext: DbContext
{
    
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=postgres;Password=Idkwhattoput979125!?;");
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public object Enrollments { get; set; }
}





