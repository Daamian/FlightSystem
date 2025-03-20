namespace NeucaFlightSystem.Domain.DiscountRule;

using Booking;
using Tenant;

public class AfricaThursdayDiscount : IDiscountRule
{
    public bool AppliesTo(Booking booking, Tenant tenant)
    {
        return booking.Route.To == "Africa" && booking.FlightDate.DayOfWeek == DayOfWeek.Thursday;
    }
}