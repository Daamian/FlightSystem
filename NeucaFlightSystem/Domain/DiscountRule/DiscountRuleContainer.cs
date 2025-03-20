namespace NeucaFlightSystem.Domain.DiscountRule;

public class DiscountRuleContainer
{
    private readonly List<IDiscountRule> _discountRules = new()
    {
        new BirthdayDiscount(),
        new AfricaThursdayDiscount()
    };

    public List<IDiscountRule> GetRules() => _discountRules;
}