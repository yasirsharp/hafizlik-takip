using Core.Entities;

namespace Entities.Concrete
{
    public class Student : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ParentName { get; set; }
        public string? ParentPhone { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentJuz { get; set; }
        public int? ClassId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }
    }
}
