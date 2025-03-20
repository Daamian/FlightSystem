namespace NeucaFlightSystem.Domain.Flight;

using Price;

public record FlightPrice(Price BasePrice, DateOnly Date);