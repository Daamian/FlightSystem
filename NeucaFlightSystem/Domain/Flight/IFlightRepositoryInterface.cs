namespace NeucaFlightSystem.Domain.Flight;

public interface IFlightRepositoryInterface
{
    public Task Add(Flight flight);
    public Task<Flight?> Find(string flightId);
}