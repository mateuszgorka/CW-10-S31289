namespace TripsAPI.Models;

public class Country
{
    public int IdCountry { get; set; }
    public string Name { get; set; }

    public ICollection<Trip> Trips { get; set; }
}