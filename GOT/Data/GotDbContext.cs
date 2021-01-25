using GOT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Data
{
    public class GotDbContext:DbContext
    {
        public GotDbContext(DbContextOptions<GotDbContext> options) : base(options) { }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Path> Paths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().HasData(
                new Area() { AreaId = 1, AreaName = "Beskidy Zachodnie" },
                new Area() { AreaId = 2, AreaName = "Beskidy Wschodnie" },
                new Area() { AreaId = 3, AreaName = "Tatry" },
                new Area() { AreaId = 4, AreaName = "Sudety" },
                new Area() { AreaId = 5, AreaName = "Gory Swietokrzyskie" }
            );

            modelBuilder.Entity<Path>()
                .HasOne(p => p.CheckpointA)
                .WithMany(t => t.PathsA)
                .HasForeignKey(m => m.CheckpointAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Path>()
                .HasOne(p => p.CheckpointB)
                .WithMany(t => t.PathsB)
                .HasForeignKey(m => m.CheckpointBId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
