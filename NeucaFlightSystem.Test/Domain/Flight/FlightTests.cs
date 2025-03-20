namespace NeucaFlightSystem.Test.Domain.Flight;

using NeucaFlightSystem.Domain.Flight;
using NeucaFlightSystem.Domain.Flight.Exception;
using NeucaFlightSystem.Domain.Price;
using NeucaFlightSystem.Domain.Route;

public class FlightTests
{
    [Fact]
    public void TestShouldInitializeFlightCorrectly()
    {
        // Arrange
        var flightId = new FlightId("ABC12345XYZ");
        var route = new Route("WAW", "JFK");
        var basePrice = new Price(500, Currency.USD);
        var departures = new List<Departure> { new(DayOfWeek.Monday, new TimeOnly(10, 0)) };
        
        // Act
        var flight = new Flight(flightId, route, basePrice, departures);
        
        // Assert
        Assert.Equal(flightId, flight.Id);
        Assert.Equal(route, flight.Route);
        Assert.Equal(basePrice, flight.BasePrice);
        Assert.Single(flight.Departures);
    }
    
    [Fact]
    public void TestShouldAddPriceForValidDate()
    {
        // Arrange
        var flight = new Flight(
            new FlightId("ABC12345XYZ"), 
            new Route("WAW", "JFK"), 
            new Price(500, Currency.USD),
            new List<Departure> { new(DayOfWeek.Monday, new TimeOnly(10, 0)) }
        );
        var flightPrice = new FlightPrice(new Price(450, Currency.USD), new DateOnly(2025, 3, 17)); // Monday
        
        // Act
        flight.AddFlightPrice(flightPrice);
        
        // Assert
        Assert.Single(flight.FlightPrices);
        Assert.Equal(flightPrice, flight.FlightPrices.First());
    }
    
    [Fact]
    public void TestShouldThrowExceptionForInvalidDate()
    {
        // Arrange
        var flight = new Flight(
            new FlightId("ABC12345XYZ"), 
            new Route("WAW", "JFK"), 
            new Price(500, Currency.USD),
            new List<Departure> { new(DayOfWeek.Monday, new TimeOnly(10, 0)) }
        );
        var invalidFlightPrice = new FlightPrice(new Price(450, Currency.USD), new DateOnly(2025, 3, 18)); // Tuesday
        
        // Act & Assert
        var ex = Assert.Throws<FlightPriceMustBeContainsInDepartureDaysException>(() => flight.AddFlightPrice(invalidFlightPrice));
        Assert.Contains("not contains in departure flight days", ex.Message);
    }
    
    [Fact]
    public void TestShouldReturnFlightPriceIfExists()
    {
        // Arrange
        var flight = new Flight(
            new FlightId("ABC12345XYZ"), 
            new Route("WAW", "JFK"), 
            new Price(500, Currency.USD),
            new List<Departure> { new(DayOfWeek.Monday, new TimeOnly(10, 0)) }
        );
        var flightPrice = new FlightPrice(new Price(450, Currency.USD), new DateOnly(2025, 3, 17)); // Monday
        flight.AddFlightPrice(flightPrice);
        
        // Act
        var price = flight.GetPriceByDate(new DateOnly(2025, 3, 17));
        
        // Assert
        Assert.Equal(450, price.Amount);
        Assert.Equal(Currency.USD, price.Currency);
    }
    
    [Fact]
    public void TestShouldReturnBasePriceIfNoSpecificPrice()
    {
        // Arrange
        var flight = new Flight(
            new FlightId("ABC12345XYZ"), 
            new Route("WAW", "JFK"), 
            new Price(500, Currency.USD),
            new List<Departure> { new(DayOfWeek.Monday, new TimeOnly(10, 0)) }
        );
        
        // Act
        var price = flight.GetPriceByDate(new DateOnly(2025, 3, 17));
        
        // Assert
        Assert.Equal(500, price.Amount);
        Assert.Equal(Currency.USD, price.Currency);
    }
}
