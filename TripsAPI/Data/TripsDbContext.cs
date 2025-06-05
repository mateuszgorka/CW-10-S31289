﻿using Microsoft.EntityFrameworkCore;
using TripsAPI.Models;

namespace TripsAPI.Data;

public class TripsDbContext : DbContext
{
    public TripsDbContext(DbContextOptions<TripsDbContext> options) : base(options) {}

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTrip>()
            .HasKey(ct => new { ct.IdClient, ct.IdTrip });

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Client)
            .WithMany(c => c.ClientTrips)
            .HasForeignKey(ct => ct.IdClient);

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Trip)
            .WithMany(t => t.ClientTrips)
            .HasForeignKey(ct => ct.IdTrip);

        modelBuilder.Entity<Trip>()
            .HasMany(t => t.Countries)
            .WithMany(c => c.Trips)
            .UsingEntity(j => j.ToTable("TripCountry")); 

        base.OnModelCreating(modelBuilder);
    }

}