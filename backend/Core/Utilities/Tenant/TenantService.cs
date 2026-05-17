namespace Core.Utilities.Tenant
{
    public class TenantService : ITenantService
    {
        public int TenantId { get; private set; }
        public int UserId { get; private set; }

        public void SetTenant(int tenantId, int userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}
