namespace NeucaFlightSystem.Application.Booking.Command.Exception;

public class TenantNotFoundException: System.Exception
{
    public TenantNotFoundException(string message) : base(message)
    {
    }
}