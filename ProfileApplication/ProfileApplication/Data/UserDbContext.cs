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
        public DbSet<Project> Projects { get; set; }
    }
}
