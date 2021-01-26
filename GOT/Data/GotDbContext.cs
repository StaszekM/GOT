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
        public DbSet<Trip> Trips { get; set; }
        public DbSet<PathTrip> PathTrips { get; set; }

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

            modelBuilder.Entity<Trip>().HasData(
                new Trip() { TripId = 1, StartDate = new DateTime(2020, 11, 19), EndDate = new DateTime(2020, 11, 24), Score = 10 },
                new Trip() { TripId = 2, StartDate = new DateTime(2020, 11, 20), EndDate = new DateTime(2020, 11, 25), Score = 20 },
                new Trip() { TripId = 3, StartDate = new DateTime(2020, 11, 21), EndDate = new DateTime(2020, 11, 26), Score = 30 },
                new Trip() { TripId = 4, StartDate = new DateTime(2020, 11, 22), EndDate = new DateTime(2020, 11, 27), Score = 40 },
                new Trip() { TripId = 5, StartDate = new DateTime(2020, 11, 23), EndDate = new DateTime(2020, 11, 28), Score = 50 }
);
        }
    }

}
