namespace NeucaFlightSystem.Domain.Tenant;

public record TenantId(Guid Value)
{
    public static TenantId New()
    {
        return new TenantId(Guid.NewGuid());
    }
}