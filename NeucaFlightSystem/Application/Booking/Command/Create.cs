namespace NeucaFlightSystem.Application.Booking.Command;

public record Create(
    string FlightId,
    DateOnly Date,
    string TenantId
);