using Microsoft.EntityFrameworkCore;
using Storage.DataTables;

namespace Storage
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserData> UserData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserData>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
