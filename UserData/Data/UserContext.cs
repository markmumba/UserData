using Microsoft.EntityFrameworkCore;
using UserData.Models;

namespace UserData.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
