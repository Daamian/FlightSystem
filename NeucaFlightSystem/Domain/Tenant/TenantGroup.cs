namespace NeucaFlightSystem.Domain.Tenant;

public class TenantGroup
{
    public string Name { private set; get; }
    public bool TrackDiscount { private set; get; }

    public TenantGroup(string name, bool trackDiscount)
    {
        Name = name;
        TrackDiscount = trackDiscount;
    }
}