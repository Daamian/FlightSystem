namespace NeucaFlightSystem.Test.Domain.Booking;

using NeucaFlightSystem.Domain.DiscountRule;
using NeucaFlightSystem.Domain.Flight;
using NeucaFlightSystem.Domain.Price;
using NeucaFlightSystem.Domain.Route;
using NeucaFlightSystem.Domain.Tenant;
using NeucaFlightSystem.Domain.Booking;

public class BookingTests
{
    [Fact]
    public void TestShouldInitializeBookingCorrectly()
    {
        // Arrange
        var flightId = new FlightId("ABC12345XYZ");
        var route = new Route("WAW", "JFK");
        var tenant = new Tenant("John Doe", new TenantGroup("VIP", true), new DateOnly());
        var basePrice = new Price(500, Currency.USD);
        var discountRules = new List<IDiscountRule>();
        var flightDate = new DateOnly(2025, 3, 17);
        
        // Act
        var booking = new Booking(flightId, flightDate, route, tenant, basePrice, discountRules);
        
        // Assert
        Assert.Equal(flightId, booking.FlightId);
        Assert.Equal(flightDate, booking.FlightDate);
        Assert.Equal(route, booking.Route);
        Assert.Equal(tenant.Id, booking.TenantId);
        Assert.Equal(basePrice, booking.FinalPrice);
    }
    
    [Fact]
    public void TestShouldApplyDiscountRule()
    {
        // Arrange
        var flightId = new FlightId("ABC12345XYZ");
        var route = new Route("WAW", "JFK");
        var basePrice = new Price(500, Currency.USD);
        var flightDate = new DateOnly(2025, 3, 17);
        var discountRules = new List<IDiscountRule> { new BirthdayDiscount() };
        var tenant = new Tenant("John Doe", new TenantGroup("VIP", true), flightDate);
        
        // Act
        var booking = new Booking(flightId, flightDate, route, tenant, basePrice, discountRules);
        
        // Assert
        Assert.Equal(495, booking.FinalPrice.Amount);
        Assert.Contains("BirthdayDiscount", booking.AppliedDiscounts);
    }
}