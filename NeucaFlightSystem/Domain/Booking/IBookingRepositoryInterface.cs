namespace NeucaFlightSystem.Domain.Booking;

public interface IBookingRepositoryInterface
{
    public Task Add(Booking booking);
}