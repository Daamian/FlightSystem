using NeucaFlightSystem.Application.Booking.Command.Exception;
using NeucaFlightSystem.Domain.DiscountRule;
using NeucaFlightSystem.Domain.Flight;
using NeucaFlightSystem.Domain.Tenant;

namespace NeucaFlightSystem.Application.Booking.Command.Handler;

using NeucaFlightSystem.Domain.Booking;

public class CreateHandler
{
    private readonly IBookingRepositoryInterface _bookingRepository;
    private readonly IFlightRepositoryInterface _flightRepository;
    private readonly ITenantRepositoryInterface _tenantRepository;
    private readonly DiscountRuleContainer _discountRuleContainer;

    public CreateHandler(
        IBookingRepositoryInterface bookingRepository, 
        IFlightRepositoryInterface flightRepository, 
        ITenantRepositoryInterface tenantRepository,
        DiscountRuleContainer discountRuleContainer
    ) {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _tenantRepository = tenantRepository;
        _discountRuleContainer = discountRuleContainer;
    }

    public async Task Handle(Create command)
    {
        var flight = await _flightRepository.Find(command.FlightId);
        if (flight is null)
        {
            throw new FlightNotFoundException($"Flight with id {command.FlightId} not found");
        }

        var tenant = await _tenantRepository.Find(new Guid(command.TenantId));
        if (tenant is null)
        {
            throw new TenantNotFoundException($"Tenant with id {command.TenantId} not found");
        }
        
        await _bookingRepository.Add(new Booking(
            new FlightId(command.FlightId),
            command.Date,
            flight.Route,
            tenant,
            flight.GetPriceByDate(command.Date),
            _discountRuleContainer.GetRules()
        ));
    }
}