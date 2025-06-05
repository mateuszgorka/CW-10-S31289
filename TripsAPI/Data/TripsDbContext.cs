using Microsoft.EntityFrameworkCore;
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
    // Mapowanie nazw tabel
    modelBuilder.Entity<Client>().ToTable("Client");
    modelBuilder.Entity<Trip>().ToTable("Trip");
    modelBuilder.Entity<ClientTrip>().ToTable("Client_Trip");
    modelBuilder.Entity<Country>().ToTable("Country");

    // Relacja Trip ↔ Country przez tabelę pośrednią
    modelBuilder.Entity<Trip>()
        .HasMany(t => t.Countries)
        .WithMany(c => c.Trips)
        .UsingEntity<Dictionary<string, object>>(
            "Country_Trip",
            j => j.HasOne<Country>().WithMany().HasForeignKey("IdCountry"),
            j => j.HasOne<Trip>().WithMany().HasForeignKey("IdTrip"),
            j =>
            {
                j.HasKey("IdCountry", "IdTrip");

                // SEED dla many-to-many
                j.HasData(
                    new { IdCountry = 1, IdTrip = 1 },
                    new { IdCountry = 2, IdTrip = 2 }
                );
            });

    // Klucz złożony ClientTrip
    modelBuilder.Entity<ClientTrip>().HasKey(ct => new { ct.IdClient, ct.IdTrip });

    modelBuilder.Entity<ClientTrip>()
        .HasOne(ct => ct.Client)
        .WithMany(c => c.ClientTrips)
        .HasForeignKey(ct => ct.IdClient);

    modelBuilder.Entity<ClientTrip>()
        .HasOne(ct => ct.Trip)
        .WithMany(t => t.ClientTrips)
        .HasForeignKey(ct => ct.IdTrip);

    // SEED danych głównych
    modelBuilder.Entity<Client>().HasData(
        new Client
        {
            IdClient = 1,
            FirstName = "John",
            LastName = "Smith",
            Email = "john@example.com",
            Telephone = "123-456-789",
            Pesel = "90010112345"
        },
        new Client
        {
            IdClient = 2,
            FirstName = "Anna",
            LastName = "Kowalska",
            Email = "anna@example.com",
            Telephone = "987-654-321",
            Pesel = "92020298765"
        });

    modelBuilder.Entity<Trip>().HasData(
        new Trip
        {
            IdTrip = 1,
            Name = "Rome",
            Description = "Trip to Rome",
            DateFrom = new DateTime(2025, 7, 1),
            DateTo = new DateTime(2025, 7, 10),
            MaxPeople = 20
        },
        new Trip
        {
            IdTrip = 2,
            Name = "Berlin",
            Description = "Trip to Berlin",
            DateFrom = new DateTime(2025, 8, 5),
            DateTo = new DateTime(2025, 8, 12),
            MaxPeople = 25
        });

    modelBuilder.Entity<Country>().HasData(
        new Country { IdCountry = 1, Name = "Italy" },
        new Country { IdCountry = 2, Name = "Germany" });

    modelBuilder.Entity<ClientTrip>().HasData(
        new ClientTrip
        {
            IdClient = 1,
            IdTrip = 1,
            RegisteredAt = new DateTime(2025, 5, 1),
            PaymentDate = new DateTime(2025, 5, 5)
        },
        new ClientTrip
        {
            IdClient = 2,
            IdTrip = 2,
            RegisteredAt = new DateTime(2025, 6, 1),
            PaymentDate = null
        });

   
    modelBuilder.Ignore<DTOs.AssignClientDto>();
    modelBuilder.Ignore<DTOs.ClientDto>();
    modelBuilder.Ignore<DTOs.TripDto>();

    base.OnModelCreating(modelBuilder);
}



}