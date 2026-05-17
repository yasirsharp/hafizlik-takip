using System.Security.Claims;
using Core.Utilities.Tenant;

namespace WebAPI.Middleware
{
    /// <summary>
    /// JWT token'dan TenantId ve UserId'yi cozumler ve ITenantService'e set eder.
    /// TenantId ASLA istemciden (header/body) alinmaz, her zaman token'dan okunur.
    /// Anonymous endpoint'ler icin (login/register) tenant bilgisi set edilmez.
    /// </summary>
    public class TenantResolverMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var tenantIdClaim = context.User.FindFirst("TenantId");
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (tenantIdClaim != null && userIdClaim != null)
                {
                    var tenantId = int.Parse(tenantIdClaim.Value);
                    var userId = int.Parse(userIdClaim.Value);
                    tenantService.SetTenant(tenantId, userId);
                }
            }

            await _next(context);
        }
    }
}
