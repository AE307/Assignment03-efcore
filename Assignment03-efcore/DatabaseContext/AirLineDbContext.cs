using Assignment03_efcore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.DatabaseContext
{
    internal class AirLineDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=airline(Assigment03);Trusted_Connection=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>().OwnsOne(a=> a.AircraftCrew);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Airline)
                .WithMany(al => al.Employees)
                .HasForeignKey(e => e.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aircraft>()
                .HasOne(ac => ac.Airline)
                .WithMany(al => al.Aircrafts)
                .HasForeignKey(ac => ac.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Airline)
                .WithMany(al => al.Transactions)
                .HasForeignKey(t => t.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RouteAssignment>()
                .HasKey(ra => new { ra.RouteId, ra.AircraftId });

            modelBuilder.Entity<RouteAssignment>()
                .HasOne(ra => ra.Route)
                .WithMany(r => r.AircraftAssignments)
                .HasForeignKey(ra => ra.RouteId);

            modelBuilder.Entity<RouteAssignment>()
                .HasOne(ra => ra.Aircraft)
                .WithMany(a => a.RouteAssignments)
                .HasForeignKey(ra => ra.AircraftId);
        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteAssignment> RouteAssignments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
