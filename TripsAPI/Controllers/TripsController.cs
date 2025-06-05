using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripsAPI.Data;
using TripsAPI.DTOs;

namespace TripsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly TripsDbContext _context;

    public TripsController(TripsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(int page = 1, int pageSize = 10)
    {
        var total = await _context.Trips.CountAsync();
        var trips = await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
            .Include(t => t.Countries)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.Countries.Select(c => c.Name).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList()
            }).ToListAsync();

        return Ok(new
        {
            pageNum = page,
            pageSize,
            allPages = (int)Math.Ceiling(total / (double)pageSize),
            trips
        });
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, AssignClientDto dto)
    {
        var existing = await _context.Clients
            .FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);
        if (existing != null)
            return BadRequest("Klient z takim PESEL-em istnieje");

        var trip = await _context.Trips
            .Include(t => t.ClientTrips)
            .FirstOrDefaultAsync(t => t.IdTrip == idTrip);

        if (trip == null || trip.DateFrom <= DateTime.Now)
            return BadRequest("Wycieczka nie istnieje lub juz sie rozpoczela");

        var client = new Models.Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        _context.ClientTrips.Add(new Models.ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = dto.PaymentDate
        });

        await _context.SaveChangesAsync();
        return Ok("Klient przypisany");
    }
}
