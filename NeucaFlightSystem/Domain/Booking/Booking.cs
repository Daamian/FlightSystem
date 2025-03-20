using System.Runtime.CompilerServices;
using NeucaFlightSystem.Domain.DiscountRule;

namespace NeucaFlightSystem.Domain.Booking;

using Flight;
using Route;
using Price;
using Tenant;

public class Booking
{
    private static readonly decimal MinPrice = 20m;
    private static readonly decimal DiscountAmount = 5m;
    
    public BookingId Id { private set; get; }
    public FlightId FlightId { private set; get; }
    public DateOnly FlightDate { private set; get; }
    public Route Route { private set; get; }
    public TenantId TenantId { private set; get; }
    public Price FinalPrice { private set; get; }
    private List<string> _appliedDiscounts = new();
    public IReadOnlyList<string> AppliedDiscounts => _appliedDiscounts.AsReadOnly();
        
    public Booking(FlightId flightId, DateOnly flightDate, Route route, Tenant tenant, Price basePrice, List<IDiscountRule> discountRules)
    {
        Id = BookingId.New();
        FlightId = flightId;
        FlightDate = flightDate;
        Route = route;
        TenantId = tenant.Id;
        CalculateFinalPrice(basePrice, tenant, discountRules);
    }
        
    private void CalculateFinalPrice(Price basePrice, Tenant tenant, List<IDiscountRule> discountRules)
    {
        decimal discountedPrice = basePrice.Amount;
            
        foreach (var rule in discountRules)
        {
            if (rule.AppliesTo(this, tenant) && discountedPrice - DiscountAmount >= MinPrice)
            {
                discountedPrice -= DiscountAmount;
                if (tenant.Group.TrackDiscount)
                {
                    _appliedDiscounts.Add(rule.GetType().Name);
                }
            }
        }

        FinalPrice = new Price(discountedPrice, basePrice.Currency);
    }
}