namespace Core.Entities
{
    /// <summary>
    /// TenantId kolonuna sahip tum entity'ler bu interface'i implement eder.
    /// DbContext'in Global Query Filter'i bu marker'i kullanir.
    /// </summary>
    public interface IHasTenant
    {
        int TenantId { get; set; }
    }
}
