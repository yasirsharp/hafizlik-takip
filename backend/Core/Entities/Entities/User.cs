using Core.Entities;

namespace Core.Utilities.Security.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }  // 0 = system-level (developer/admin)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }   // developer, admin, yonetici, ogretici, yardimci_ogretici, veli, ogrenci
    }
}
