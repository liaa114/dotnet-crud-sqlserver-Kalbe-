using Employe.Models;
using Microsoft.EntityFrameworkCore;

namespace Employe.Data
{
    public class EmployeContext : DbContext
    {
        public EmployeContext(DbContextOptions<EmployeContext> options) : base(options) { }

        public DbSet<MST_Asset> MST_Asset { get; set; }

        public DbSet<MST_User> MST_User { get; set; }

    }
}