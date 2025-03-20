namespace NeucaFlightSystem.Domain.Booking;

public record BookingId(Guid Value)
{
    public static BookingId New()
    {
        return new BookingId(Guid.NewGuid());
    }
}