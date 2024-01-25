using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musclegym.Models;

namespace Musclegym.Data
{
    public class MusclegymContext : DbContext
    {
        public MusclegymContext (DbContextOptions<MusclegymContext> options)
            : base(options)
        {
        }

        public DbSet<Musclegym.Models.Member> Member { get; set; } = default!;
        public DbSet<Registrasi> Registrasis { get; set; }
        public DbSet<Promo> Promos { get; set; }


    }
}
