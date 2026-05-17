using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;

namespace Core.Utilities.Tenant
{
    public static class TenantResolver
    {
        public static int GetCurrentTenantId()
        {
            try
            {
                var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var tenantService = httpContextAccessor?.HttpContext?.RequestServices.GetService<ITenantService>();
                return tenantService?.TenantId ?? 0;
            }
            catch
            {
                return 0; // Fallback during migrations/tests or without HTTP context
            }
        }
    }
}
