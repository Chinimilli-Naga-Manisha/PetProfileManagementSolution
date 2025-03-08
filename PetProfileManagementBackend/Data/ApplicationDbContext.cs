using Microsoft.EntityFrameworkCore;
using PetProfileManagementBackend.Models;

namespace PetProfileManagementBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pets> Pets { get; set; }
    }
}
