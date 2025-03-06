using Microsoft.EntityFrameworkCore;
using PetProfileManagementBackend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PetProfileManagementBackend.Data
{

    public class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here
        public DbSet<Pet> Pet { get; set; }
    }
}
