using Microsoft.EntityFrameworkCore;
using Penga.Domain;

namespace Penga.Infrastructure
{
    public class PengaDbContext : DbContext
    {
        public DbSet<CostCategory> CostCategories { get; set; }

        public PengaDbContext(DbContextOptions<PengaDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
