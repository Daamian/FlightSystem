namespace NeucaFlightSystem.Application.Booking.Command.Exception;

public class FlightNotFoundException : System.Exception
{
    public FlightNotFoundException(string message) : base(message)
    {
    }
}