using NeucaFlightSystem.Domain.DiscountRule;
using NeucaFlightSystem.Domain.Price;
using NeucaFlightSystem.Domain.Route;

namespace NeucaFlightSystem.Application.Flight.Command.Handler;

using NeucaFlightSystem.Domain.Flight;

public class CreateHandler
{
    private readonly IFlightRepositoryInterface _flightRepository;

    public CreateHandler(IFlightRepositoryInterface flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task Handle(Create command)
    {
        var flight = new Flight(
            new FlightId(command.FlightId),
            new Route(command.From, command.To),
            new Price(command.BasePrice, Enum.Parse<Currency>(command.CurrencySymbol, true)),
            command.Departures.Select(d => new Departure(
                d.Day,
                d.DepartureTime
            )).ToList()
        );

        foreach (var price in command.Prices)
        {
            flight.AddFlightPrice(new FlightPrice(
                new Price(price.Price, Enum.Parse<Currency>(command.CurrencySymbol, true)), 
                price.Date
            ));
        }
        
        await _flightRepository.Add(flight);
    }
}