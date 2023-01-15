using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Models
{
    public class VSASDbContext : DbContext
    {
        public VSASDbContext(DbContextOptions options) : base(options)
        {

        }

       public DbSet<Registration> Registrations { get; set; }
       public DbSet<UserDetails> UserDetails { get; set; }
       public DbSet<VehicleDetail> VehicleDetail { get; set; }
    }
}
