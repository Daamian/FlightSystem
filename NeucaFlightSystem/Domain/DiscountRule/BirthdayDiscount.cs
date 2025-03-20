namespace NeucaFlightSystem.Domain.DiscountRule;

using Booking;
using Tenant;

public class BirthdayDiscount : IDiscountRule
{
    public bool AppliesTo(Booking booking, Tenant tenant)
    {
        return tenant.Birthday == booking.FlightDate;
    }
}