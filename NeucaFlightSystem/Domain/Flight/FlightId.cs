namespace NeucaFlightSystem.Domain.Flight;

public record FlightId
{
    public string Value;
    
    public FlightId(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid Flight ID format");
        Value = value;
    }
        
    private static bool IsValid(string value)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Z]{3}\d{5}[A-Z]{3}$");    
    }
}