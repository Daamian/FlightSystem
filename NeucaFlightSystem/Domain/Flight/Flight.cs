using NeucaFlightSystem.Domain.Flight.Exception;

namespace NeucaFlightSystem.Domain.Flight;

using Price;
using Route;

public class Flight
{
    public FlightId Id { private set; get; }
    public Route Route { private set; get; }
    public Price BasePrice { private set; get; }
    private List<Departure> _departures = new();
    public IReadOnlyList<Departure> Departures => _departures.AsReadOnly();
    
    private List<FlightPrice> _flightPrices = new();
    public IReadOnlyList<FlightPrice> FlightPrices => _flightPrices.AsReadOnly();
        
    public Flight(FlightId id, Route route, Price basePrice, List<Departure> departures)
    {
        Id = id;
        Route = route;
        BasePrice = basePrice;
        _departures = departures;
    }

    public void AddFlightPrice(FlightPrice flightPrice)
    {
       if (!_departures.Exists(d => d.Day == flightPrice.Date.DayOfWeek))
       {
           throw new FlightPriceMustBeContainsInDepartureDaysException($"Date {flightPrice.Date} not contains in departure flight days");
       }

       _flightPrices.RemoveAll(fp => fp.Date == flightPrice.Date);
       _flightPrices.Add(flightPrice);
    }

    public Price GetPriceByDate(DateOnly date)
    {
        var flightPrice = _flightPrices.FirstOrDefault(fp => fp.Date == date);
        return flightPrice is not null ? flightPrice.BasePrice : BasePrice;
    }
}