namespace Core.Utilities.Tenant
{
    /// <summary>
    /// HttpContext'ten cozumlenen TenantId ve UserId'yi tum katmanlara tasir.
    /// Middleware tarafindan set edilir, Business/DataAccess tarafindan okunur.
    /// </summary>
    public interface ITenantService
    {
        int TenantId { get; }
        int UserId { get; }
        void SetTenant(int tenantId, int userId);
    }
}
