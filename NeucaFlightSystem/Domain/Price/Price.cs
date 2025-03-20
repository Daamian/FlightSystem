namespace NeucaFlightSystem.Domain.Price;

public record Price
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }

    public Price(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentException("Price amount cannot be negative.", nameof(amount));

        Amount = amount;
        Currency = currency;
    }
}