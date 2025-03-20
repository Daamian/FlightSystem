namespace NeucaFlightSystem.Domain.Tenant;

public interface ITenantRepositoryInterface
{
    public Task<Tenant?> Find(Guid id);
}