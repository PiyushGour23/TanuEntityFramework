using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Model;

namespace TanuEntityFramework
{
    public class TanuDbContext : DbContext
    {
        public TanuDbContext(DbContextOptions<TanuDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
