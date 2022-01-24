using Microsoft.EntityFrameworkCore;

namespace Rest_API_Profiles_Users.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
