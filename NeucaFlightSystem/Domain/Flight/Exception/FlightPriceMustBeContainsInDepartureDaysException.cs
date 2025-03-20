using NeucaFlightSystem.Domain.Exception;

namespace NeucaFlightSystem.Domain.Flight.Exception;

public class FlightPriceMustBeContainsInDepartureDaysException : DomainException
{
    public FlightPriceMustBeContainsInDepartureDaysException(string message) : base(message)
    {
    }
}