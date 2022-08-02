
using Microsoft.EntityFrameworkCore;
using NetCoreJwtAuth.Entities;

namespace NetCoreJwtAuth.DataContext
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext()
        {

        }


        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Dusan; Database=jwtTest; Trusted_Connection=True;");

        }

        public DbSet<User> User { get; set; }

    }
}