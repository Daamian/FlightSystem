using NeucaFlightSystem.Domain.Price;

namespace NeucaFlightSystem.Application.Flight.Command;

public record Create(
    string FlightId,
    string From,
    string To,
    decimal BasePrice,
    string CurrencySymbol,
    List<(DayOfWeek Day, TimeOnly DepartureTime)> Departures,
    List<(decimal Price, string CurrencySymbol, DateOnly Date)> Prices
);