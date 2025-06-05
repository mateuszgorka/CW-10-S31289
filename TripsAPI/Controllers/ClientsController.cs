using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripsAPI.Data;

namespace TripsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TripsDbContext _context;

    public ClientsController(TripsDbContext context)
    {
        _context = context;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
            return NotFound("Client not found.");

        if (client.ClientTrips.Any())
            return BadRequest("Client is registered for trips.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    // zmiany wazne
}