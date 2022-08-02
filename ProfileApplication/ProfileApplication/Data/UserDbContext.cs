using Microsoft.EntityFrameworkCore;
using ProfileApplication.Models;

namespace ProfileApplication.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //    const string connectionString = "server=localhost;port=3306;user=root;password=MyPassword;database=userdb";
        //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        //        .UseLoggerFactory(LoggerFactory.Create(b => b
        //            .AddConsole()
        //            .AddFilter(level => level >= LogLevel.Information)))
        //        .EnableSensitiveDataLogging()
        //        .EnableDetailedErrors();
        //}
    }
}
