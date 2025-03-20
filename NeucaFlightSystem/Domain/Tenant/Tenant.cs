namespace NeucaFlightSystem.Domain.Tenant;

public class Tenant
{
    public TenantId Id { private set; get; }
    public string Name { private set; get; }
    public DateOnly Birthday { private set; get; }
    public TenantGroup Group { private set; get; }
        
    public Tenant(string name, TenantGroup group, DateOnly birthday)
    {
        Id = TenantId.New();
        Name = name;
        Group = group;
        Birthday = birthday;
    }
}