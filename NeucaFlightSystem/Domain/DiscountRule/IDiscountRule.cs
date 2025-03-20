namespace NeucaFlightSystem.Domain.DiscountRule;

using Booking;
using Tenant;

public interface IDiscountRule
{
    bool AppliesTo(Booking booking, Tenant tenant);
}