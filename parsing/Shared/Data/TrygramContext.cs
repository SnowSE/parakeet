using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Data
{
    public class TrygramContext : DbContext
    {
        public TrygramContext(DbContextOptions<TrygramContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trygram>()
                .HasKey(t => t.Key);
            modelBuilder.Entity<TrygramValues>()
                .HasOne<Trygram>(tv=>tv.Trygram)
                .WithMany(t => t.Values)
                .HasForeignKey(tv => tv.TrygramKey);
        }
        public DbSet<Trygram> Trygrams { get; set; }
        public DbSet<TrygramValues> TrygramValues { get; set; }
    }
}
